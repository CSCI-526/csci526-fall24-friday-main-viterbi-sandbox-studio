using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResetGame : MonoBehaviour
{
    public Button playButton;
    public Transform player1;
    public Transform player2;
    public GameObject level1Wall;

    private Vector3 spawnPoint1Player1;
    private Vector3 spawnPoint1Player2;

    private bool spawnPointSet1 = false;

    // Start is called before the first frame update
    void Start()
    {   
        playButton.onClick.AddListener(ResetGameScene);

        // Set initial spawn point to the players' starting position
        spawnPoint1Player1 = player1.position;
        spawnPoint1Player2 = player2.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawnPointSet1 && player1.position.x > level1Wall.transform.position.x && player2.position.x > level1Wall.transform.position.x)
        {
            Debug.Log("Both players have moved beyond the Level1");
            float wallOffset = 3f;
            spawnPoint1Player1 = new Vector3(level1Wall.transform.position.x + wallOffset, player1.position.y, player1.position.z);
            spawnPoint1Player2 = new Vector3(level1Wall.transform.position.x + wallOffset, player2.position.y, player2.position.z);
            spawnPointSet1 = true;
        }
    }

    // Method to reset the game
    public void ResetGameScene()
    {
        Debug.Log("Reset Game.");
        EventSystem.current.SetSelectedGameObject(null);
        if (spawnPointSet1){
            player1.position = spawnPoint1Player1;
            player2.position = spawnPoint1Player2;
        } else {
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}