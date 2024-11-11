using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class BasicTutorialMechanicTracker : MonoBehaviour
{
    private float cameraRotationStartTime;
    private float playerMoveStartTime;
    private float playerJumpStartTime;
    private float pushboxStartTime;

    void StartCameraRotationTracking()
    {
        cameraRotationStartTime = Time.time;
    }

    void StartPlayerMoveTracking()
    {
        playerMoveStartTime = Time.time;
    }

    void StartPlayerJumpeTracking()
    {
        playerJumpStartTime = Time.time;
    }

    void StartPushBoxTracking()
    {
        pushboxStartTime = Time.time;
    }

    void SendCameraRotationEvent()
    {
        float timeSpent = Time.time - cameraRotationStartTime;
        string mechanicName = "Camera Rotation";
        TimeSpentOnLearningMechanicsEvent timeSpentOnLearningMechanicsEvent = new TimeSpentOnLearningMechanicsEvent
        {
            TimeSpent = timeSpent,
            MechanicName = mechanicName
        };
        AnalyticsService.Instance.RecordEvent(timeSpentOnLearningMechanicsEvent);
        Debug.Log($"{mechanicName} Event sent. Time spent {timeSpent}");
    }

    void SendPlayerMoveEvent()
    {
        float timeSpent = Time.time - playerMoveStartTime;
        string mechanicName = "Player Movement";
        TimeSpentOnLearningMechanicsEvent timeSpentOnLearningMechanicsEvent = new TimeSpentOnLearningMechanicsEvent
        {
            TimeSpent = timeSpent,
            MechanicName = mechanicName
        };
        AnalyticsService.Instance.RecordEvent(timeSpentOnLearningMechanicsEvent);
        Debug.Log($"{mechanicName} Event sent. Time spent {timeSpent}");
    }

    void SendPlayerJumpnEvent()
    {
        float timeSpent = Time.time - playerJumpStartTime;
        string mechanicName = "Player Jumping";
        TimeSpentOnLearningMechanicsEvent timeSpentOnLearningMechanicsEvent = new TimeSpentOnLearningMechanicsEvent
        {
            TimeSpent = timeSpent,
            MechanicName = mechanicName
        };
        AnalyticsService.Instance.RecordEvent(timeSpentOnLearningMechanicsEvent);
        Debug.Log($"{mechanicName} Event sent. Time spent {timeSpent}");
    }

    void SendPushingBoxEvent()
    {
        float timeSpent = Time.time - pushboxStartTime;
        string mechanicName = "Pushing Box";
        TimeSpentOnLearningMechanicsEvent timeSpentOnLearningMechanicsEvent = new TimeSpentOnLearningMechanicsEvent
        {
            TimeSpent = timeSpent,
            MechanicName = mechanicName
        };
        AnalyticsService.Instance.RecordEvent(timeSpentOnLearningMechanicsEvent);
        Debug.Log($"{mechanicName} Event sent. Time spent {timeSpent}");
    }
}
