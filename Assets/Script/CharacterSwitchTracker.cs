using System;
using System.IO;
using UnityEngine;

public class SimpleSwitchTracker : MonoBehaviour
{
    private int switchCount = 0;
    private string logFilePath;

    void Start()
    {
        string logDirectory = Path.Combine(Application.dataPath, "Logs");
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }
        
        logFilePath = Path.Combine(logDirectory, "GameLog.txt");
        Debug.Log("Log file path: " + logFilePath);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            switchCount++;
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string message = $"Switch {switchCount}: Character switched at {timestamp}.";
            LogMessage(message);
            Debug.Log(message);
        }
    }

    private void LogMessage(string message)
    {
        using (StreamWriter sw = File.AppendText(logFilePath))
        {
            sw.WriteLine(message);
        }
    }
}