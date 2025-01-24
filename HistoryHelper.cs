using Newtonsoft.Json;

public static class HistoryHelper
{
    private static readonly string HistoryFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "history.json");

    // 加载历史记录
    public static Dictionary<string, List<string>> LoadHistory()
    {
        if (File.Exists(HistoryFilePath))
        {
            string json = File.ReadAllText(HistoryFilePath);
            return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(json) ?? new Dictionary<string, List<string>>();
        }
        return new Dictionary<string, List<string>>();
    }

    // 保存历史记录
    public static void SaveHistory(Dictionary<string, List<string>> history)
    {
        string json = JsonConvert.SerializeObject(history, Formatting.Indented);
        File.WriteAllText(HistoryFilePath, json);
    }

    // 添加历史记录
    public static void AddToHistory(Dictionary<string, List<string>> history, string key, string value)
    {
        if (!history.ContainsKey(key))
        {
            history[key] = new List<string>();
        }

        if (!history[key].Contains(value))
        {
            history[key].Add(value);
            SaveHistory(history);
        }
    }
}