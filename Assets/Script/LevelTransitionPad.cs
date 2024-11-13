using UnityEngine;
using UnityEngine.SceneManagement;

public class PadTrigger : MonoBehaviour
{
    public GameObject playerPad; // Assign this to player1Pad or player2Pad
    public string playerName; // Set to "Player1" or "Player2"
    public string nextSceneName; // Set the name of the next scene in the Inspector
    public bool singlePlayerMode = false; // Set to true if there's only one player and one pad

    private static bool player1OnPad = false;
    private static bool player2OnPad = false;

    private GameManager gameManager;

    private void Start()
    {
        // Reset the pad states at the start of each level
        player1OnPad = false;
        player2OnPad = false;

        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == playerName)
        {
            if (playerName == "Player1")
            {
                player1OnPad = true;
                Debug.Log("Player 1 is on their pad.");
            }
            else if (playerName == "Player2")
            {
                player2OnPad = true;
                Debug.Log("Player 2 is on their pad.");
            }
        }

        CheckTransitionCondition();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == playerName)
        {
            if (playerName == "Player1")
            {
                player1OnPad = false;
                Debug.Log("Player 1 left their pad.");
            }
            else if (playerName == "Player2")
            {
                player2OnPad = false;
                Debug.Log("Player 2 left their pad.");
            }
        }
    }

    private void CheckTransitionCondition()
    {
        if (singlePlayerMode)
        {
            // Transition if in single-player mode and the player is on their pad
            if ((playerName == "Player1" && player1OnPad) || (playerName == "Player2" && player2OnPad))
            {
                Debug.Log("Single player is on their pad. Transitioning to the next scene.");
                PersistentMenu.instance.WinTransit();
            }
        }
        else
        {
            // Transition if both players are on their respective pads
            if (player1OnPad && player2OnPad)
            {
                Debug.Log("Both players are on their respective pads. Transitioning to the next scene.");
                PersistentMenu.instance.WinTransit();
            }
        }
    }
}
