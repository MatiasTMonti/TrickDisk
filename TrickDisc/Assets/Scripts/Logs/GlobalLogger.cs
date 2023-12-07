using System;
using System.IO;
using UnityEngine;

public class GlobalLogger : MonoBehaviour
{
    private static string logFilePath = Application.persistentDataPath + "/Logs.txt";

    public static void Log(string message)
    {
        string logEntry = $"{message}\n";
        File.AppendAllText(logFilePath, logEntry);
    }
}