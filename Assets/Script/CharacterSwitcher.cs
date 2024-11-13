using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject player1;    // the first character
    public GameObject player2;    // the second character (optional)
    public Camera mainCamera;     // main camera

    private GameObject activePlayer;
    private bool isControllingBoth = false;

    private LevelCompleteTracker levelCompleteTracker;
    private CameraController cameraController;

    void Start()
    {
        if (player2 != null)
        {
            ActivatePlayer(player1, player2);  // Start with player2 active by default
        }
        else
        {
            ActivatePlayer(player1, null);  // Only player1 exists
        }

        levelCompleteTracker = FindObjectOfType<LevelCompleteTracker>();
        cameraController = FindObjectOfType<CameraController>();
    }

    void Update()
    {
        if (player2 != null && Input.GetKeyDown(KeyCode.R))
        {
            if (activePlayer == player1)
            {
                ActivatePlayer(player2, player1);
            }
            else
            {
                ActivatePlayer(player1, player2);
            }
            cameraController.SwitchCameraPivot(); 

            if (levelCompleteTracker != null)
            {
                levelCompleteTracker.RecordPlayerSwitch();
            }
        }

        if (player2 != null && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            isControllingBoth = true;
            ControlBothPlayers(true);
        }
        else
        {
            isControllingBoth = false;
            ControlBothPlayers(false);
        }

        UpdateArrowVisibility();
    }

    void ActivatePlayer(GameObject newActivePlayer, GameObject oldActivePlayer)
    {
        activePlayer = newActivePlayer;
        if (isControllingBoth) return;

        if (oldActivePlayer != null)
        {
            oldActivePlayer.GetComponent<PlayerController>().enabled = false;
        }

        newActivePlayer.GetComponent<PlayerController>().enabled = true;
    }

    void ControlBothPlayers(bool controlBoth)
    {
        if (player2 == null)
        {
            player1.GetComponent<PlayerController>().enabled = true;
            return;
        }

        player1.GetComponent<PlayerController>().enabled = controlBoth || (activePlayer == player1);
        player2.GetComponent<PlayerController>().enabled = controlBoth || (activePlayer == player2);
    }

    public GameObject getActivePlayer()
    {
        return activePlayer;
    }

    void UpdateArrowVisibility()
    {
        if (player2 == null)
        {
            // Only player1 exists, so show its arrow only
            player1.GetComponent<PlayerController>().ToggleArrow(true);
            return;
        }

        if (isControllingBoth)
        {
            player1.GetComponent<PlayerController>().ToggleArrow(true);
            player2.GetComponent<PlayerController>().ToggleArrow(true);
        }
        else
        {
            player1.GetComponent<PlayerController>().ToggleArrow(activePlayer == player1);
            player2.GetComponent<PlayerController>().ToggleArrow(activePlayer == player2);
        }
    }
}
