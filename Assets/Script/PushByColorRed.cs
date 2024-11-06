using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushByColorRed : MonoBehaviour
{
    private Rigidbody rb;
    private int player1Count = 0;
    private int otherPlayerCount = 0;
    public float force = 10f;
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            player1Count++;
            check();
        }
        else if (collision.gameObject.CompareTag("Player1"))
        {
            otherPlayerCount++;
            check();
        }
    }

    void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player2"))
        {
            player1Count--;
            check();
        }
        else if (collision.gameObject.CompareTag("Player1"))
        {
            otherPlayerCount--;
            check();
        }
    }

    private void check()
    {

        if (player1Count == 1 && otherPlayerCount == 0)
        {
            rb.isKinematic = false;
        }
        else
        {

            rb.isKinematic = true;
        }
    }
    void FixedUpdate()
    {
        if (player1Count == 1 && otherPlayerCount == 0)
        {
            rb.AddForce(Vector3.forward * force);
        }
    }


}
