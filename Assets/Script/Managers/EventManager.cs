using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    // Store each tracker with a unique key, such as "LevelPassingTime_Level1"
    private Dictionary<string, IEventTracker> eventTrackers = new Dictionary<string, IEventTracker>();

    // Register trackers with a unique identifier
    public void RegisterTracker(string trackerId, IEventTracker tracker)
    {
        if (!eventTrackers.ContainsKey(trackerId))
        {
            eventTrackers.Add(trackerId, tracker);
            Debug.Log($"Add event tracker {trackerId}");
        }
    }

    // Unregister a tracker if needed
    public void UnregisterTracker(string trackerId)
    {
        if (eventTrackers.ContainsKey(trackerId))
        {
            eventTrackers.Remove(trackerId);
            Debug.Log($"Remove event tracker {trackerId}");
        }
    }

    // Send an event by tracker ID
    public void SendEvent(string trackerId)
    {
        if (eventTrackers.ContainsKey(trackerId))
        {
            eventTrackers[trackerId].SendEvent();
            Debug.Log($"Send event tracker {trackerId}");
        }
        else
        {
            Debug.LogWarning($"Tracker {trackerId} not found.");
        }
    }
}
