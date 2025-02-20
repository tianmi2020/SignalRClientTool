using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Text.Json.Nodes;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

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

            // Always use JSON parameter format
            ckJsonParameter.Checked = true;
            ckJsonParameter.Enabled = false;

            // Set parameter examples
            var examples = new StringBuilder();
            examples.AppendLine("Parameter Input Examples:");
            examples.AppendLine("1. No parameters: []");
            examples.AppendLine("2. Single string parameter: [\"myString\"]");
            examples.AppendLine("3. Single number parameter: [42]");
            examples.AppendLine("4. Single boolean parameter: [true]");
            examples.AppendLine("5. Multiple simple parameters: [\"myString\", 42, true]");
            examples.AppendLine("6. Array parameter: [[\"item1\", \"item2\", \"item3\"]]");
            examples.AppendLine("7. Single object parameter:");
            examples.AppendLine("[{");
            examples.AppendLine("  \"name\": \"myName\",");
            examples.AppendLine("  \"value\": 42");
            examples.AppendLine("}]");
            examples.AppendLine("8. Complex object parameter");
            examples.AppendLine("[{");
            examples.AppendLine("  \"Name\": \"myName\",");
            examples.AppendLine("  \"Keys\": [\"key1\", \"key2\", \"key4\"],");
            examples.AppendLine("  \"MaxNumber\": 1000,");
            examples.AppendLine("  \"MaxBytes\": 1024");
            examples.AppendLine("}]");
            examples.AppendLine("9. Multiple mixed parameters:");
            examples.AppendLine("[\"myString\", [\"array1\", \"array2\"], 42, {\"prop\": \"value\"}]");

            lblParamExample.Text = examples.ToString();
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
                  .WithAutomaticReconnect()
                  .Build();

                _hubConnection.ServerTimeout = TimeSpan.FromSeconds(3);
                _hubConnection.KeepAliveInterval = TimeSpan.FromSeconds(2);
                _hubConnection.Closed += async (error) =>
                {
                    try
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            btnConnect.Text = "Connect";
                        });

                        LogMessage("Connection closed.");
                        await Reconnect();
                    }
                    catch (Exception ex)
                    {
                        LogMessage($"Connection failed: {ex.Message}");
                    }
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
                string methodName = cmbServerMethodName.Text.Trim();
                try
                {
                    if (string.IsNullOrEmpty(methodName))
                    {
                        MessageBox.Show("Please enter a server method name.");
                        return;
                    }   
                    object? result;

                    // Handle empty message case
                    if (string.IsNullOrEmpty(txtParameter.Text) || txtParameter.Text.Trim() == "[]")
                    {
                        LogMessage($"Invoke {methodName} without parameters");
                        result = await _hubConnection.InvokeCoreAsync<object>(methodName, Array.Empty<object>());
                    }
                    else
                    {
                        try
                        {
                            // Parse the JSON array
                            var jArray = JArray.Parse(txtParameter.Text);
                            
                            // For single complex object parameter
                            if (jArray.Count == 1 && jArray[0].Type == JTokenType.Object)
                            {
                                // Convert to System.Text.Json format
                                var jsonStr = jArray[0].ToString(Formatting.None);
                                var jsonElement = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(jsonStr);
                                
                                LogMessage($"Invoke {methodName} with parameter: {jsonStr}");
                                try 
                                {
                                    result = await _hubConnection.InvokeCoreAsync<object>(methodName, new object[] { jsonElement });
                                }
                                catch (Exception invokeEx)
                                {
                                    LogMessage($"Detailed invoke error: {invokeEx.GetType().Name}: {invokeEx.Message}");
                                    if (invokeEx.InnerException != null)
                                    {
                                        LogMessage($"Inner exception: {invokeEx.InnerException.GetType().Name}: {invokeEx.InnerException.Message}");
                                    }
                                    throw;
                                }
                            }
                            else
                            {
                                // For multiple parameters
                                var parameters = new List<object>();
                                foreach (var token in jArray)
                                {
                                    if (token.Type == JTokenType.Object)
                                    {
                                        var jsonStr = token.ToString(Formatting.None);
                                        parameters.Add(System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(jsonStr));
                                    }
                                    else if (token.Type == JTokenType.Array)
                                    {
                                        var jsonStr = token.ToString(Formatting.None);
                                        parameters.Add(System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(jsonStr));
                                    }
                                    else
                                    {
                                        // For primitive types, use direct conversion
                                        parameters.Add(token.ToObject<object>());
                                    }
                                }

                                LogMessage($"Invoke {methodName} with parameters: {txtParameter.Text}");
                                try
                                {
                                    result = await _hubConnection.InvokeCoreAsync<object>(methodName, parameters.ToArray());
                                }
                                catch (Exception invokeEx)
                                {
                                    LogMessage($"Detailed invoke error: {invokeEx.GetType().Name}: {invokeEx.Message}");
                                    if (invokeEx.InnerException != null)
                                    {
                                        LogMessage($"Inner exception: {invokeEx.InnerException.GetType().Name}: {invokeEx.InnerException.Message}");
                                    }
                                    throw;
                                }
                            }
                        }
                        catch (Newtonsoft.Json.JsonException ex)
                        {
                            LogMessage($"Invalid JSON format: {ex.Message}");
                            return;
                        }
                        catch (System.Text.Json.JsonException ex)
                        {
                            LogMessage($"Invalid JSON format: {ex.Message}");
                            return;
                        }
                    }

                    LogResponse(result);
                }
                catch (Exception ex)
                {
                    var errorMessage = $"Failed to send message: {ex.GetType().Name}: {ex.Message}";
                    if (ex.InnerException != null)
                    {
                        errorMessage += $"\nInner Exception: {ex.InnerException.GetType().Name}: {ex.InnerException.Message}";
                    }
                    LogMessage(errorMessage);
                }
                // Save method name to history
                HistoryHelper.AddToHistory(_history, "ServerMethodNames", methodName);
                HistoryHelper.AddToHistory(_history, $"ServerMethodParameter_{methodName}", txtParameter.Text, false);
                //check if exist same item in the cmbServerMethodName
                if (!cmbServerMethodName.Items.Contains(methodName))
                {
                    cmbServerMethodName.Items.Add(methodName);
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
            cmbMethodName.SelectedIndex = -1;
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
            else
            {
                LogMessage($"Received response: null");
            }
        }
        /// <summary>
        /// Attempt to reconnect to SignalR Hub
        /// </summary>
        /// <returns></returns>
        private async Task Reconnect()
        {
            await Task.Delay(5000);
            if (_hubConnection != null && _hubConnection.State != HubConnectionState.Connected)
            {
                LogMessage("Reconnecting to SignalR Hub...");
                try
                {
                    await _hubConnection.StartAsync();

                    Invoke((MethodInvoker)delegate
                    {
                        btnConnect.Text = "Disconnect";
                    });
                    LogMessage("Reconnected to SignalR Hub.");
                }
                catch (Exception ex)
                {
                    LogMessage($"Reconnection failed: {ex.Message}");
                }

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtLog.Text = "";
        }

        private void cmbServerMethodName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string methodName = cmbServerMethodName.Text.Trim();
            if (_history.ContainsKey($"ServerMethodParameter_{methodName}"))
            {
                //this.txtMessage
                var ps = _history[$"ServerMethodParameter_{methodName}"].ToArray();
                if (ps.Length > 0)
                {
                    txtParameter.Text = ps[0];
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Parse input text into multiple parameters with appropriate types
        /// </summary>
        private object[] ParseParameters(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Array.Empty<object>();

            try
            {
                // Convert the input into a JSON array format
                var jsonArray = $"[{input}]";
                // Use JSON.NET to parse the parameters
                var parameters = JsonConvert.DeserializeObject<JArray>(jsonArray);
                
                if (parameters == null)
                    return Array.Empty<object>();

                // Convert each parameter to the appropriate type
                var result = new List<object>();
                foreach (var param in parameters)
                {
                    if (param.Type == JTokenType.Array)
                    {
                        // Convert array to string[]
                        result.Add(param.ToObject<string[]>());
                    }
                    else if (param.Type == JTokenType.Integer)
                    {
                        result.Add(param.ToObject<int>());
                    }
                    else if (param.Type == JTokenType.Float)
                    {
                        result.Add(param.ToObject<double>());
                    }
                    else if (param.Type == JTokenType.Boolean)
                    {
                        result.Add(param.ToObject<bool>());
                    }
                    else
                    {
                        // Default to string for other types
                        result.Add(param.ToObject<string>());
                    }
                }
                return result.ToArray();
            }
            catch (Exception ex)
            {
                LogMessage($"Error parsing parameters: {ex.Message}");
                return Array.Empty<object>();
            }
        }
    }
}