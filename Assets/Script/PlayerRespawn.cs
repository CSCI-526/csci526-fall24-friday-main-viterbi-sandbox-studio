using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    private Vector3 _respawnPoint1;
    private Vector3 _respawnPoint2;
    private Vector3 _respawnOffset = new Vector3(0, 0, 3);

    private void Start()
    {
        // Set the initial spawn point at the start of the game
        _respawnPoint1 = player1.transform.position;
        if (player2 != null)
        {
            _respawnPoint2 = player2.transform.position;
        }
    }

    public void SetRespawnPoint(Vector3 newRespawnPoint)
    {
        _respawnPoint1 = newRespawnPoint;
        if (player2 != null)
        {
            _respawnPoint2 = newRespawnPoint;
        }
    }

    public void Respawn()
    {
        player1.transform.position = _respawnPoint1;
        player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (player2 != null)
        {
            player2.transform.position = _respawnPoint2;
            player2.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
