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

    private CharacterSwitchTracker characterSwitchTracker;

    void Start()
    {
        if (player2 != null)
        {
            ActivatePlayer(player2, player1);  // Start with player2 active by default
        }
        else
        {
            ActivatePlayer(player1, null);  // Only player1 exists
        }

        characterSwitchTracker = FindObjectOfType<CharacterSwitchTracker>();
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

            if (characterSwitchTracker != null)
            {
                characterSwitchTracker.RecordPlayerSwitch();
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
        if (isControllingBoth || newActivePlayer == null) return;

        if (oldActivePlayer != null)
        {
            oldActivePlayer.GetComponent<PlayerController>().enabled = false;
        }

        newActivePlayer.GetComponent<PlayerController>().enabled = true;
        activePlayer = newActivePlayer;
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
