using System.IO;

public class BasicLogger : Logger
{
    private string path;

    private string logs;

    public BasicLogger(string path, TMPro.TextMeshProUGUI text)
    {
        this.text = text;
        this.path = path;

        if (File.Exists(path))
            logs = File.ReadAllText(path);
    }

    public override void Clear()
    {
        logs = "";

        text.text = "";

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public override void ShowAllLogs()
    {
        text.text = logs;
    }

    public override void AddLog(string log)
    {
        logs += log + "\n";
    }

    public override void WriteLog()
    {
        File.WriteAllText(path, logs);
    }

    public override string GetLogText()
    {
        return logs;
    }
}
