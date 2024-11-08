using System;
using System.IO;
using Unity.Services.Analytics;
using UnityEngine;

public class CharacterSwitchTracker : MonoBehaviour
{
    private int switchCount = 0;
    //private string logFilePath;

    void Start()
    {
        //string logDirectory = Path.Combine(Application.dataPath, "Logs");
        //if (!Directory.Exists(logDirectory))
        //{
        //    Directory.CreateDirectory(logDirectory);
        //}
        
        //logFilePath = Path.Combine(logDirectory, "GameLog.txt");
        //Debug.Log("Log file path: " + logFilePath);
    }

    void Update()
    {

    }

    //private void LogMessage(string message)
    //{
    //    using (StreamWriter sw = File.AppendText(logFilePath))
    //    {
    //        sw.WriteLine(message);
    //    }
    //}

    private void ResetSwitchCount()
    {
        switchCount = 0;
    }

    public void RecordPlayerSwitch()
    {
        switchCount++;
        //string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //string message = $"Switch {switchCount}: Character switched at {timestamp}.";
        //LogMessage(message);
        //Debug.Log(message);
    }

    public void SendCharacterSwitchEvent(int currentLevel)
    {
        CharacterSwitchCountsEvent characterSwitchCountsEvent = new CharacterSwitchCountsEvent
        {
            CharacterSwitchCounts = switchCount,
            CurrentLevel = currentLevel
        };

        AnalyticsService.Instance.RecordEvent(characterSwitchCountsEvent);
        Debug.Log($"characterSwitchCountsEvent sent. Current level {currentLevel}, switchCount {switchCount}");
        ResetSwitchCount();
    }
}