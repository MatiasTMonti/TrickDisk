using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI text;
    [SerializeField] private TMPro.TMP_InputField LogInput;

    private Logger _instance;

    public Logger Instance { get { return _instance; } }

    private void Awake()
    {
        Debug.Log(Application.persistentDataPath);

        _instance = Logger.CreateLogger(text);
    }

    private void Start()
    {
        _instance.ShowAllLogs();
    }

    public void SendLogsButtonPressed()
    {
        _instance.AddLog(LogInput.text);
        _instance.WriteLog();
        LogInput.text = _instance.GetLogText();
        _instance.ShowAllLogs();
        LogInput.text = "";
    }

    public void ClearLogsButtonPressed()
    {
        _instance.Clear();
    }
}