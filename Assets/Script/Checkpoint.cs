using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private LevelManager levelManager;
    private PlayerRespawn playerRespawn;
    private bool isPassed = false;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        playerRespawn = FindObjectOfType<PlayerRespawn>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isPassed && other.CompareTag("Player"))
        {
            Debug.Log("pass checkpoint");
            levelManager.LevelUp();
            if (playerRespawn != null)
            {
                Debug.Log("set new respawn position to: " + transform.position);
                playerRespawn.SetRespawnPoint(transform.position);
            }
            isPassed = true;
            FindObjectOfType<LevelResetManager>().ResetCurrentLevelObjects();
        }
    }
}
