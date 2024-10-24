﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRise : MonoBehaviour
{
    public Transform platform; 
    public float riseHeight = 2.0f; 
    public float riseSpeed = 1.0f; 


    private bool triggered = false;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = platform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!triggered)
            {
                triggered = true;
                StartCoroutine(RisePlatform());
            }
        }
    }

    IEnumerator RisePlatform()
    {
        yield return new WaitForSeconds(3);
        
        float elapsedTime = 0;
        while (elapsedTime < riseHeight / riseSpeed)
        {
            platform.position = Vector3.Lerp(startPosition, new Vector3(startPosition.x, startPosition.y + riseHeight, startPosition.z), (elapsedTime / (riseHeight / riseSpeed)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        platform.position = new Vector3(startPosition.x, startPosition.y + riseHeight, startPosition.z);
    }
}
