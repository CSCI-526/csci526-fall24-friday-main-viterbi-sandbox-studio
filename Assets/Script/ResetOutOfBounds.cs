using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOutOfBounds : MonoBehaviour
{
    public float yBoundary = -10f;
    public GameObject player1;
    public GameObject player2; // optional

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player1 != null && IsPlayerOutOfBound(player1))
        {
            gameManager.RestartLevel();
        }
        else if (player2 != null && IsPlayerOutOfBound(player2))
        {
            gameManager.RestartLevel();
        }
    }

    bool IsPlayerOutOfBound(GameObject player)
    {
        return player.transform.position.y < yBoundary;
    }
}
