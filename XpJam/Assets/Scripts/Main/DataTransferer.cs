using System.Collections.Generic;

public static class DataTransferer
{
    public static readonly Dictionary<string, string> Data = new Dictionary<string, string>();
    public static void UpdateData(string key, string data)
    {
        if (Data.ContainsKey(key)) Data[key] = data;
        else Data.Add(key, data);
    }
}
