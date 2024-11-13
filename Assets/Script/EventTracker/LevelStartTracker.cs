using System;
using System.IO;
using Unity.Services.Analytics;
using UnityEngine;

public class LevelStartTracker : MonoBehaviour, IEventTracker
{
    private int currentLevel;
    private string currentLevelName;
    private LevelManager levelManager;

    public void Initialize(int levelId)
    {
        levelManager = FindObjectOfType<LevelManager>();
        currentLevel = levelId;
        currentLevelName = levelManager.GetCurrentLevelName();
    }

    void Update()
    {

    }

    public void ResetTracker()
    {

    }

    public void SendEvent()
    {
        LevelStartEvent LevelStartEvent = new LevelStartEvent
        {
            CurrentLevel = currentLevel,
            CurrentLevelName = currentLevelName
        };

        AnalyticsService.Instance.RecordEvent(LevelStartEvent);
        Debug.Log($"LevelStartEvent sent. Current level {currentLevelName}");
    }
}