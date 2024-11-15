using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerManager : MonoBehaviour
{
    public TutorialEventTrigger[] tutorialEventTriggers;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var tutorialEventTrigger in tutorialEventTriggers)
        {
            tutorialEventTrigger.InitializeTrigger();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
