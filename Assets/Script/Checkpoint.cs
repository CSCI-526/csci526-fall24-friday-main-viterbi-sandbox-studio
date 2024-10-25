using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private LevelManager levelManager;
    private PlayerRespawn playerRespawn;
    private CharacterSwitchTracker characterSwitchTracker;

    private bool isPassed = false;
    private int passedPlayerCount = 0;
    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        playerRespawn = FindObjectOfType<PlayerRespawn>();
        characterSwitchTracker = FindObjectOfType<CharacterSwitchTracker>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isPassed && other.CompareTag("Player"))
        {
            passedPlayerCount++;
            if (passedPlayerCount == 2)
            {
                if (levelManager != null)
                {
                    if (characterSwitchTracker != null)
                    {
                        int currentLevel = levelManager.getLevel();
                        characterSwitchTracker.SendCharacterSwitchEvent(currentLevel);
                    }
                    TriggerLevelUp();
                }
                if (playerRespawn != null)
                {
                    UpdateRespawnPoint();
                }
                isPassed = true;
            }
        }
    }

    private void TriggerLevelUp()
    {
        levelManager.LevelUp();
        FindObjectOfType<LevelResetManager>().ResetCurrentLevelObjects();
    }

    private void UpdateRespawnPoint()
    {
        playerRespawn.SetRespawnPoint(transform.position);
    }
}
