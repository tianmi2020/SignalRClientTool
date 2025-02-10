using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Text.Json.Nodes;
using System.Text;

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

            _history = HistoryHelper.LoadHistory();

            if (_history.ContainsKey("HubUrls"))
            {
                cmbHubUrl.Items.AddRange(_history["HubUrls"].ToArray());
            }

            if (_history.ContainsKey("MethodNames"))
            {
                cmbMethodName.Items.AddRange(_history["MethodNames"].ToArray());
            }

            if (_history.ContainsKey("ServerMethodNames"))
            {
                cmbServerMethodName.Items.AddRange(_history["ServerMethodNames"].ToArray());
            }
        }
        /// <summary>
        /// Connect to or disconnect from SignalR Hub
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

                _hubConnection = new HubConnectionBuilder()
                  .WithUrl(hubUrl)
                  .Build();


                _hubConnection.Closed += async (error) =>
                {
                    LogMessage("Connection closed.");
                    await Reconnect();
                };

                try
                {

                    await _hubConnection.StartAsync();
                    LogMessage("Connected to SignalR Hub.");
                    btnConnect.Text = "Disconnect";

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

                await _hubConnection.StopAsync();
                _hubConnection = null;
                LogMessage("Disconnected from SignalR Hub.");
                btnConnect.Text = "Connect";
            }

        }

        /// <summary>
        /// Send message to SignalR Hub
        /// </summary>
        private async void btnSend_Click(object sender, EventArgs e)
        {
            // Check connection status
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

                    object? result;
                    // Handle empty message case
                    if (string.IsNullOrEmpty(txtMessage.Text))
                    {
                        result = await _hubConnection.InvokeAsync<object>(methodName);
                        LogResponse(result);
                    }
                    // Handle JSON formatted message
                    else if (IsValidJson(txtMessage.Text))
                    {
                        JsonDocument jsonDocument = JsonDocument.Parse(txtMessage.Text);
                        LogMessage($"Sent message: {jsonDocument.RootElement}");
                        result = await _hubConnection.InvokeAsync<object>(methodName, jsonDocument);
                        LogResponse(result);
                    }
                    // Handle plain text message
                    else
                    {
                        LogMessage($"Sent message: {txtMessage.Text}");
                        result = await _hubConnection.InvokeAsync<object>(methodName, txtMessage.Text);
                        LogResponse(result);
                    }

                    // Save method name to history
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
        /// Bind message listener to SignalR Hub
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


            if (_listeners.ContainsKey(methodName))
            {
                _listeners[methodName].Dispose();
                _listeners.Remove(methodName);
                LogMessage($"Stopped listening to method: {methodName}");
            }
            else
            {

                // Create new message listener
                var listener = _hubConnection.On<object>(methodName, (message) =>
                {
                    // Process received message
                    if (message is JsonElement jsonElement)
                    {
                        string value = FormatJsonResponse(jsonElement);
                        LogMessage($"Received message from {methodName}: {value}");
                    }
                    else
                    {
                        LogMessage($"Received message from {methodName}: {message}");
                    }
                });


                _listeners[methodName] = listener;
                LogMessage($"Listening to method: {methodName}");

                HistoryHelper.AddToHistory(_history, "MethodNames", methodName);
            }

        }
        /// <summary>
        /// Remove message listener
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
        /// Format JsonElement response to readable string
        /// </summary>
        /// <param name="jsonElement">The JSON element to be formatted</param>
        /// <returns>Formatted string</returns>
        private string FormatJsonResponse(JsonElement jsonElement)
        {
            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.Number:
                    if (jsonElement.TryGetInt64(out long longValue))
                        return longValue.ToString();
                    if (jsonElement.TryGetDouble(out double doubleValue))
                        return doubleValue.ToString();
                    return jsonElement.GetRawText();

                case JsonValueKind.String:
                    return jsonElement.GetString() ?? "";

                case JsonValueKind.True:
                    return "true";

                case JsonValueKind.False:
                    return "false";

                case JsonValueKind.Null:
                    return "null";

                case JsonValueKind.Object:
                case JsonValueKind.Array:
                    using (var stream = new MemoryStream())
                    {
                        using (var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true }))
                        {
                            jsonElement.WriteTo(writer);
                        }
                        return Encoding.UTF8.GetString(stream.ToArray());
                    }

                default:
                    return jsonElement.GetRawText();
            }
        }

        /// <summary>
        /// Log server response to message log
        /// </summary>
        /// <param name="result">Response object from server</param>
        private void LogResponse(object? result)
        {
            if (result != null)
            {
                if (result is JsonElement jsonElement)
                {
                    string value = FormatJsonResponse(jsonElement);
                    LogMessage($"Received response: {value}");
                }
                else
                {
                    LogMessage($"Received response: {result}");
                }
            }
        }
        /// <summary>
        /// Attempt to reconnect to SignalR Hub
        /// </summary>
        /// <returns></returns>
        private async Task Reconnect()
        {
            await Task.Delay(5000);
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
        /// Add message to log with timestamp
        /// </summary>
        /// <param name="message">Message to be logged</param>
        private void LogMessage(string message)
        {

            Invoke((MethodInvoker)delegate
            {
                txtLog.AppendText($"{DateTime.Now}: {message}{Environment.NewLine}");
            });
        }
        /// <summary>
        /// Check if a string is valid JSON format
        /// </summary>
        /// <param name="input">String to be checked</param>
        /// <returns>true if valid JSON, false otherwise</returns>
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