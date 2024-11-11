using System;
using System.IO;
using Unity.Services.Analytics;
using UnityEngine;

public class CharacterSwitchTracker : MonoBehaviour
{
    private int switchCount = 0;

    private LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {

    }

    private void ResetSwitchCount()
    {
        switchCount = 0;
    }

    public void RecordPlayerSwitch()
    {
        switchCount++;
    }

    public void SendCharacterSwitchEvent()
    {
        string currentLevelName = levelManager.GetCurrentLevelName();
        CharacterSwitchFrequencyEvent characterSwitchFrequencyEvent = new CharacterSwitchFrequencyEvent
        {
            CharacterSwitchCounts = switchCount,
            CurrentLevelName = currentLevelName
        };

        AnalyticsService.Instance.RecordEvent(characterSwitchFrequencyEvent);
        Debug.Log($"characterSwitchCountsEvent sent. Current level {currentLevelName}, switchCount {switchCount}");
        ResetSwitchCount();
    }
}