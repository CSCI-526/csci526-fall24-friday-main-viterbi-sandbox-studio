using System;
using System.IO;
using Unity.Services.Analytics;
using UnityEngine;

public class CharacterSwitchTracker : MonoBehaviour, IEventTracker
{
    private int switchCount = 0;
    private string trackerId;
    private LevelManager levelManager;

    public void Initialize(int levelId)
    {
        levelManager = FindObjectOfType<LevelManager>();

        trackerId = $"CharacterSwitchCount_{levelId}";
        EventManager.Instance.RegisterTracker(trackerId, this);
    }

    void Update()
    {

    }

    public void RecordPlayerSwitch()
    {
        switchCount++;
    }

    public void SendEvent()
    {
        string currentLevelName = levelManager.GetCurrentLevelName();
        CharacterSwitchFrequencyEvent characterSwitchFrequencyEvent = new CharacterSwitchFrequencyEvent
        {
            CharacterSwitchCounts = switchCount,
            CurrentLevelName = currentLevelName
        };

        AnalyticsService.Instance.RecordEvent(characterSwitchFrequencyEvent);
        Debug.Log($"characterSwitchCountsEvent sent. Current level {currentLevelName}, switchCount {switchCount}");
    }
}