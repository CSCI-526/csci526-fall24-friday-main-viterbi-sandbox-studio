using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject player1;    // the first character
    public GameObject player2;    // the second character
    public Camera mainCamera;     // main camera

    private GameObject activePlayer;

    private bool isControllingBoth = false;  

    void Start()
    {
        ActivatePlayer(player2, player1);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (activePlayer == player1)
            {
                ActivatePlayer(player2, player1);
            }
            else
            {
                ActivatePlayer(player1, player2);
            }
        }
        
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
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
        
        if (isControllingBoth) return;

        
        oldActivePlayer.GetComponent<PlayerController>().enabled = false;
        
        newActivePlayer.GetComponent<PlayerController>().enabled = true;

        
        activePlayer = newActivePlayer;
    }

    
    void ControlBothPlayers(bool controlBoth)
    {
        if (controlBoth)
        {
            
            player1.GetComponent<PlayerController>().enabled = true;
            player2.GetComponent<PlayerController>().enabled = true;
        }
        else
        {
            
            player1.GetComponent<PlayerController>().enabled = (activePlayer == player1);
            player2.GetComponent<PlayerController>().enabled = (activePlayer == player2);
        }
    }

    public GameObject getActivePlayer()
    {
        return activePlayer;
    }

    
    void UpdateArrowVisibility()
    {
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
