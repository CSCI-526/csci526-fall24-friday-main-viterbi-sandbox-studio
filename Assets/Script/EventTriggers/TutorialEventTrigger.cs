using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialEventTrigger : MonoBehaviour
{
    protected TutorialTracker prevTutorialTracker;
    protected TutorialTracker currTutorialTracker;
    private bool hasTriggered;
    // Start is called before the first frame update
    void Start()
    {
        hasTriggered = false;
    }

    public abstract void InitializeTrigger();

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered || !other.gameObject.CompareTag("Player"))
        {
            return;
        }
        Debug.Log("Tutorial Event Triggered");
        hasTriggered = true;

        if (prevTutorialTracker != null)
        {
            prevTutorialTracker.SendEvent();
        }
        if (currTutorialTracker != null)
        {
            currTutorialTracker.StartTracking();
        }
    }
}
