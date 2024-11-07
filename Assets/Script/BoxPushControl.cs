using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPushControl : MonoBehaviour
{
     
    private int playerCount = 0; 
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; 
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCount = playerCount + 1;
            Check();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCount = playerCount - 1; ;
            Check();
        }
    }
    
    private void Check()
    {
        
        if (playerCount > 1)
        {
            rb.isKinematic = false; 
        }
        else if(playerCount == 0)
        {
            rb.isKinematic = false;
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.isKinematic = true; 
        }
    }

    
}
