using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpEndEventTrigger : TutorialEventTrigger
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void InitializeTrigger()
    {
        prevTutorialTracker = FindObjectOfType<PlayerJumpTutorialTracker>();
        currTutorialTracker = null;
    }
}
