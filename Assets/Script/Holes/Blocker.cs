using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    public string ObjectName = "player2"; 

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.name.Equals(ObjectName))
        {
            
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {   
                rb.velocity = Vector3.zero; 
                Vector3 direction = (other.transform.position - transform.position).normalized;
                other.transform.position -= direction * 0.1f;

            }

            
        }
    }
}
