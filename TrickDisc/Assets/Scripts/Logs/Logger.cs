using UnityEngine;

public abstract class Logger
{
    private static string path = Application.persistentDataPath + "/Logs.txt";

    protected TMPro.TextMeshProUGUI text;

    public abstract void AddLog(string log);

    public abstract void ShowAllLogs();

    public abstract void WriteLog();

    public abstract void Clear();

    public abstract string GetLogText();

    public static Logger CreateLogger(TMPro.TextMeshProUGUI text)
    {
#if UNITY_ANDROID
        return new AndroidLogger(path, text);
#else
            return new BasicLogger(path, text);
#endif
    }
}