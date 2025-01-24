using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Text.Json;

namespace SignalRClientTool
{
    public partial class MainForm : Form
    {
        private Dictionary<string, IDisposable> _listeners = new Dictionary<string, IDisposable>();
        private Dictionary<string, List<string>> _history;
        private HubConnection? _hubConnection;
        public MainForm()
        {
            InitializeComponent();
            // ������ʷ��¼
            _history = HistoryHelper.LoadHistory();
            // ���� Hub URL ��ʷ��¼
            if (_history.ContainsKey("HubUrls"))
            {
                cmbHubUrl.Items.AddRange(_history["HubUrls"].ToArray());
            }
            // ���ط���������ʷ��¼
            if (_history.ContainsKey("MethodNames"))
            {
                cmbMethodName.Items.AddRange(_history["MethodNames"].ToArray());
            } 
            // ���ط���˷���������ʷ��¼
            if (_history.ContainsKey("ServerMethodNames"))
            {
                cmbServerMethodName.Items.AddRange(_history["ServerMethodNames"].ToArray());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnConnect_Click(object sender, EventArgs e)
        {
            string hubUrl = cmbHubUrl.Text.Trim();
            if (string.IsNullOrEmpty(hubUrl))
            {
                MessageBox.Show("Please enter a Hub URL.");
                return;
            }
            if (_hubConnection == null)
            {
                // ��ʼ�� SignalR ����
                _hubConnection = new HubConnectionBuilder()
                  .WithUrl(hubUrl)
                  .Build();

                // �������ӹر��¼�
                _hubConnection.Closed += async (error) =>
                {
                    LogMessage("Connection closed.");
                    await Reconnect();
                };

                try
                {
                    // ��������
                    await _hubConnection.StartAsync();
                    LogMessage("Connected to SignalR Hub.");
                    btnConnect.Text = "Disconnect";
                    // ���� Hub URL ����ʷ��¼
                    HistoryHelper.AddToHistory(_history, "HubUrls", hubUrl);
                }
                catch (Exception ex)
                {
                    LogMessage($"Connection failed: {ex.Message}");
                    _hubConnection = null;
                }
            }
            else
            {
                // �Ͽ�����
                await _hubConnection.StopAsync();
                _hubConnection = null;
                LogMessage("Disconnected from SignalR Hub.");
                btnConnect.Text = "Connect";
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (_hubConnection != null && _hubConnection.State == HubConnectionState.Connected)
            {
                try
                {
                    string methodName = cmbServerMethodName.Text.Trim();

                    if (string.IsNullOrEmpty(methodName))
                    {
                        MessageBox.Show("Please enter a server method name.");
                        return;
                    }  
                    if (string.IsNullOrEmpty(txtMessage.Text))
                    {
                        return;
                    }

                    // ��̬�ж���������
                    if (IsValidJson(txtMessage.Text))
                    {
                        JsonDocument jsonDocument = JsonDocument.Parse(txtMessage.Text);
                        LogMessage($"Sent message: {jsonDocument.RootElement}");
                        // ���� SignalR ����
                        await _hubConnection.InvokeAsync(methodName, jsonDocument);
                    }
                    else
                    {
                        LogMessage($"Sent message: {txtMessage.Text}");
                        // ������Ϣ
                        await _hubConnection.InvokeAsync(methodName, txtMessage.Text);
                    }
                 
                    HistoryHelper.AddToHistory(_history, "ServerMethodNames", methodName);

                }
                catch (Exception ex)
                {
                    LogMessage($"Failed to send message: {ex.Message}");
                }
            }
            else
            {
                LogMessage("Not connected to SignalR Hub.");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBindListener_Click(object sender, EventArgs e)
        {
            string methodName = cmbMethodName.Text.Trim();

            if (string.IsNullOrEmpty(methodName))
            {
                MessageBox.Show("Please enter a method name.");
                return;
            }
            if (_hubConnection == null || _hubConnection.State != HubConnectionState.Connected)
            {
                LogMessage("Not connected to SignalR Hub.");
                return;
            }

            // ����Ѿ����˸÷�������ȡ����
            if (_listeners.ContainsKey(methodName))
            {
                _listeners[methodName].Dispose();
                _listeners.Remove(methodName);
                LogMessage($"Stopped listening to method: {methodName}");
            }
            else
            {
                // ��̬���µļ�����
                var listener = _hubConnection.On<object>(methodName, (message) =>
                {
                    if (message is JsonElement jsonElement)
                    {
                        // ��ȡԭʼֵ
                        if (jsonElement.ValueKind == JsonValueKind.Number)
                        {
                            int value = jsonElement.GetInt32();
                            LogMessage($"Received message from {methodName}: {value}");
                        }
                        else if (jsonElement.ValueKind == JsonValueKind.String)
                        {
                            string value = jsonElement.GetString();
                            LogMessage($"Received message from {methodName}: {value}");
                        }
                        else if (jsonElement.ValueKind == JsonValueKind.Object)
                        {
                            string value = JsonConvert.SerializeObject(jsonElement.GetRawText());
                            LogMessage($"Received message from {methodName}: {value}");
                        }
                        else
                        {
                            LogMessage($"Received unknown message type from {methodName}: {jsonElement.ValueKind}");
                        }
                    }
                    else
                    {
                        LogMessage($"Received message from {methodName}: {message}");
                    }
                });

                // ���������
                _listeners[methodName] = listener;
                LogMessage($"Listening to method: {methodName}");

                // ���淽�����Ƶ���ʷ��¼
                HistoryHelper.AddToHistory(_history, "MethodNames", methodName);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveListener_Click(object sender, EventArgs e)
        {
            string methodName = cmbMethodName.Text.Trim();

            if (string.IsNullOrEmpty(methodName))
            {
                MessageBox.Show("Please enter a method name.");
                return;
            }
            if (_listeners.ContainsKey(methodName))
            {
                _listeners[methodName].Dispose();
                _listeners.Remove(methodName);
                LogMessage($"Stopped listening to method: {methodName}");
            }
            else
            {
                LogMessage($"No listener found for method: {methodName}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task Reconnect()
        {
            await Task.Delay(5000); // �ȴ� 5 �������
            if (_hubConnection != null)
            {
                LogMessage("Reconnecting to SignalR Hub...");
                try
                {
                    await _hubConnection.StartAsync();
                }
                catch (Exception ex)
                {
                    LogMessage($"Reconnection failed: {ex.Message}");
                }
                LogMessage("Reconnected to SignalR Hub.");
            }
            else
            {
                LogMessage("No SignalR Hub connection to reconnect.");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void LogMessage(string message)
        {
            // �� UI �߳��и�����־
            Invoke((MethodInvoker)delegate
            {
                txtLog.AppendText($"{DateTime.Now}: {message}{Environment.NewLine}");
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool IsValidJson(string input)
        {
            try
            {
                JsonDocument.Parse(input);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}