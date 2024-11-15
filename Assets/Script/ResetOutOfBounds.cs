using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOutOfBounds : MonoBehaviour
{
    private float yBoundary = -20f;
    public GameObject player1;
    public GameObject player2; // optional

    private LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player1 != null && IsPlayerOutOfBound(player1))
        {
            levelManager.ResetPlayerAndObjects();
        }
        else if (player2 != null && IsPlayerOutOfBound(player2))
        {
            levelManager.ResetPlayerAndObjects();
        }
    }

    bool IsPlayerOutOfBound(GameObject player)
    {
        return player.transform.position.y < yBoundary;
    }
}
