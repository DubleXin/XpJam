using System.Collections.Generic;

public static class DataTransferer
{
    public static readonly Dictionary<string, object> Data = new Dictionary<string, object>();
    public static void UpdateData(string key, object data)
    {
        if (Data.ContainsKey(key)) Data[key] = data;
        else Data.Add(key, data);
    }
}
