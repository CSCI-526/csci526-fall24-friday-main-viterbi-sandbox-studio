using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventObjectOverlap : MonoBehaviour
{
    // Tag of the obstacle 
    public string obstacleTag = "Untagged";

    private Vector3 startPosition;
    //private float halfSize;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        /*
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            halfSize = collider.bounds.extents.x;
        }
        else
        {
            Debug.LogError("No Collider Component!");
            halfSize = 0f;
        }
        */

        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 endPosition = transform.position;
        Vector3 direction = endPosition - startPosition;
        float distance = direction.magnitude;

        if (distance > 0)
        {
            //  Use rays to detect 
            RaycastHit hit;
            if (Physics.Raycast(startPosition, direction.normalized, out hit, distance))
            {
                if (hit.collider.CompareTag(obstacleTag))
                {
                    //Vector3 hitPosition = hit.point - transform.forward * halfSize;
                    transform.position = hit.point;

                    // Stop the object
                    if (rb != null)
                    {
                        rb.velocity = Vector3.zero;
                    }

                    
                    
                }
            }
        }

        startPosition = endPosition;
    }
    /*
    private Vector3 GetFrontPosition()
    {
        return transform.position + transform.forward * halfSize;
    }
    */
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(obstacleTag))
        {
            //Vector3 lastPosition = startPosition - transform.forward * halfSize;
            transform.position = startPosition;
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }

      
        }
    }
}
