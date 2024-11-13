using System;
using System.IO;
using Unity.Services.Analytics;
using UnityEngine;

public class LevelCompleteTracker : MonoBehaviour, IEventTracker
{
    private int switchCount;
    private float previousTime;
    private int restartLevelCount;
    private int currentLevel;
    private string currentLevelName;
    private LevelManager levelManager;

    public void Initialize(int levelId)
    {
        levelManager = FindObjectOfType<LevelManager>();
        currentLevel = levelId;
        currentLevelName = levelManager.GetCurrentLevelName();
        InitializeParams();
    }

    void Update()
    {

    }

    public void InitializeParams()
    {
        restartLevelCount = 0;
        switchCount = 0;
        previousTime = Time.time;
    }

    public void RecordPlayerSwitch()
    {
        switchCount++;
    }

    public void ResetTracker()
    {
        restartLevelCount++;
        switchCount = 0;
        previousTime = Time.time;
    }

    public void SendEvent()
    {
        float timeSpent = Time.time - previousTime;
        LevelCompleteEvent levelCompleteEvent = new LevelCompleteEvent
        {
            RestartLevelCounts = restartLevelCount,
            CharacterSwitchCounts = switchCount,
            TimeSpent = timeSpent,
            CurrentLevel = currentLevel,
            CurrentLevelName = currentLevelName
        };

        AnalyticsService.Instance.RecordEvent(levelCompleteEvent);
        Debug.Log($"levelCompleteEvent sent. Current level {currentLevelName}, restart counts {restartLevelCount}, switchCount {switchCount}, timeSpent {timeSpent}");
    }
}