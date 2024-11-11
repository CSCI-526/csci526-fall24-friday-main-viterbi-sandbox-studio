using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class LiftPlaformTutorialMechanicTracker : MonoBehaviour
{
    private float startTime;

    void StartTracking()
    {
        startTime = Time.time;
    }

    void SendEvent()
    {
        float timeSpent = Time.time - startTime;
        string mechanicName = "Platform Lift";
        TimeSpentOnLearningMechanicsEvent timeSpentOnLearningMechanicsEvent = new TimeSpentOnLearningMechanicsEvent
        {
            TimeSpent = timeSpent,
            MechanicName = mechanicName
        };
        AnalyticsService.Instance.RecordEvent(timeSpentOnLearningMechanicsEvent);
        Debug.Log($"{mechanicName} Event sent. Time spent {timeSpent}");
    }
}
