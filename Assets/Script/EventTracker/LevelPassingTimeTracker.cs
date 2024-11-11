using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class LevelPassingTimeTracker : MonoBehaviour, IEventTracker
{
    private float previousTime;
    private string trackerId;
    private LevelManager levelManager;
    // Start is called before the first frame update
    public void Initialize(int levelId)
    {
        levelManager = FindObjectOfType<LevelManager>();

        previousTime = Time.time;
        trackerId = $"LevelPassingTime_{levelId}";
        EventManager.Instance.RegisterTracker(trackerId, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        EventManager.Instance.UnregisterTracker(trackerId);
    }

    public void SendEvent()
    {
        float timeSpent = Time.time - previousTime;
        string currentLevelName = levelManager.GetCurrentLevelName();
        LevelPassingTimeEvent levelPassingTimeEvent = new LevelPassingTimeEvent
        {
            TimeSpent = timeSpent,
            CurrentLevelName = currentLevelName
        };

        AnalyticsService.Instance.RecordEvent(levelPassingTimeEvent);
        Debug.Log($"levelPassingTimeEvent sent. Current level {currentLevelName}, time spent {timeSpent.ToString("0.00")}");
    }
}
