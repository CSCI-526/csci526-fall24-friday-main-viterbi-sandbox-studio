using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventObjectOverlap : MonoBehaviour
{
    // Tag of the obstacle 
    public string obstacleTag = "Untagged";

    private Vector3 startPosition;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();


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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(obstacleTag))
        {

            transform.position = startPosition;
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }


        }
    }
}
