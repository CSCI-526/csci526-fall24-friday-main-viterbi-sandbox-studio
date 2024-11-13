using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushByColorRed : MonoBehaviour
{
    private Rigidbody rb;
    private int player1Count = 0;
    private int otherPlayerCount = 0;
    
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player2")
        {
            player1Count++;
            check();
        }
        else 
        {
            otherPlayerCount++;
            check();
        }
    }

    void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.name == "Player2")
        {
            player1Count--;
            check();
        }
        else 
        {
            otherPlayerCount--;
            check();
        }
    }

    private void check()
    {

        if (player1Count == 1)
        {
            rb.isKinematic = false;
        }
        else if (player1Count == 0 && otherPlayerCount == 0)
        {
            rb.isKinematic = false;
            //rb.velocity = Vector3.zero;
        }
        else
        {

            rb.isKinematic = true;
        }
    }
    


}
