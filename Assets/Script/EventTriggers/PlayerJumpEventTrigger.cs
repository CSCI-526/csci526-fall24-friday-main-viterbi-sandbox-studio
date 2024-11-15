using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpEventTrigger : TutorialEventTrigger
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void InitializeTrigger()
    {
        prevTutorialTracker = null;
        currTutorialTracker = FindObjectOfType<PlayerJumpTutorialTracker>();
    }
}