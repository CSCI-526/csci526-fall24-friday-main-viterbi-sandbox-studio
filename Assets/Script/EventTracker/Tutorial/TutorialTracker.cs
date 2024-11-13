using UnityEngine;
using Unity.Services.Analytics;

public abstract class TutorialTracker : MonoBehaviour
{
    // Protected fields so they can be accessed by subclasses
    protected float startTime;
    protected string mechanicName;

    public void StartTracking()
    {
        startTime = Time.time;
    }

    public void SendEvent()
    {
        float timeSpent = Time.time - startTime;
        TimeSpentOnLearningMechanicsEvent timeSpentOnLearningMechanicsEvent = new TimeSpentOnLearningMechanicsEvent
        {
            TimeSpent = timeSpent,
            MechanicName = mechanicName
        };
        AnalyticsService.Instance.RecordEvent(timeSpentOnLearningMechanicsEvent);
        Debug.Log($"{mechanicName} Event sent. Time spent {timeSpent}");
    }
}