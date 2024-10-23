using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOutOfBounds : MonoBehaviour
{
    public float yBoundary = -10f;
    public GameObject player1;
    public GameObject player2;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.transform.position.y < yBoundary || player2.transform.position.y < yBoundary)
        {
            gameManager.RestartLevel();
        }
    }
}
