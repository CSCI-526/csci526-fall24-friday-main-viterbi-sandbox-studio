using System;
using System.IO;
using Unity.Services.Analytics;
using UnityEngine;

public class LevelCompleteTracker : MonoBehaviour, IEventTracker
{
    private int switchCount = 0;
    private float previousTime;
    private int currentLevel;
    private string currentLevelName;
    private LevelManager levelManager;

    public void Initialize(int levelId)
    {
        levelManager = FindObjectOfType<LevelManager>();
        currentLevel = levelId;
        currentLevelName = levelManager.GetCurrentLevelName();
        previousTime = Time.time;
    }

    void Update()
    {

    }

    public void RecordPlayerSwitch()
    {
        switchCount++;
    }

    public void ResetTracker()
    {
        switchCount = 0;
        previousTime = Time.time;
    }

    public void SendEvent()
    {
        float timeSpent = Time.time - previousTime;
        LevelCompleteEvent levelCompleteEvent = new LevelCompleteEvent
        {
            CharacterSwitchCounts = switchCount,
            TimeSpent = timeSpent,
            CurrentLevel = currentLevel,
            CurrentLevelName = currentLevelName
        };

        AnalyticsService.Instance.RecordEvent(levelCompleteEvent);
        Debug.Log($"levelCompleteEvent sent. Current level {currentLevelName}, switchCount {switchCount}, timeSpent {timeSpent}");
    }
}