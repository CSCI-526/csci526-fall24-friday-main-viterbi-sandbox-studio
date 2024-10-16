using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject player1;    // the first character
    public GameObject player2;    // the second character
    public Camera mainCamera;     // main camera

    public Vector3 cameraOffset1 = new Vector3(0, 5, 11);
    public Vector3 cameraOffset2 = new Vector3(0, 5, -10);

    public Quaternion cameraRotation1 = new Quaternion(0.02621f, 0.95009f, -0.08312f, 0.29956f);
    public Quaternion cameraRotation2 = new Quaternion(0.08501f, 0.21950f, -0.01920f, 0.97171f);

    private GameObject activePlayer;
    public bool isPlayer1Playing;

    private bool isControllingBoth = false;  

    void Start()
    {
        
        activePlayer = player1;
        ActivatePlayer(player1, player2);
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

        UpdateCameraPosition();
    }

    
    void ActivatePlayer(GameObject newActivePlayer, GameObject oldActivePlayer)
    {
        
        if (isControllingBoth) return;

        
        oldActivePlayer.GetComponent<PlayerController>().enabled = false;
        
        newActivePlayer.GetComponent<PlayerController>().enabled = true;

        
        activePlayer = newActivePlayer;
        isPlayer1Playing = (newActivePlayer == player1);
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

    
    void UpdateCameraPosition()
    {
        if (activePlayer != null)
        {
            Vector3 desiredPosition;
            Quaternion desiredRotation;
            if (activePlayer == player1)
            {
                desiredPosition = activePlayer.transform.position + cameraOffset1;
                desiredRotation = cameraRotation1;
            }
            else
            {
                desiredPosition = activePlayer.transform.position + cameraOffset2;
                desiredRotation = cameraRotation2;
            }
            mainCamera.transform.position = desiredPosition;
            mainCamera.transform.rotation = desiredRotation;

        }
    }
}
