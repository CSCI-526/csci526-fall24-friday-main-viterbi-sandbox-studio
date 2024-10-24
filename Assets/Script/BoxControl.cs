using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionStay(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Untagged"))
        {
            rb.velocity = Vector3.zero;        
            rb.angularVelocity = Vector3.zero; 

            
            gameObject.tag = "Untagged";
        }
    }

    void OnCollisionExit(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Untagged"))
        {
            gameObject.tag = "BoxLevelF";  
        }
    }
}
