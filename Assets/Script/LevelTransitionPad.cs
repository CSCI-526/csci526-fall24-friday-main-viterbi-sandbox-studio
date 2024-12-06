using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PadTrigger : MonoBehaviour
{
    public GameObject playerPad; // Assign this to player1Pad or player2Pad
    public string playerName; // Set to "Player1" or "Player2"
    public string nextSceneName; // Set the name of the next scene in the Inspector
    public bool singlePlayerMode = false; // Set to true if there's only one player and one pad
    public AudioClip transitionSound; // The sound to play during level transition

    private AudioSource audioSource; // Audio source to play the sound
    private static bool player1OnPad = false;
    private static bool player2OnPad = false;

    public BeaconController beaconControllerBlue;
    public BeaconController beaconControllerRed;
    private GameManager gameManager;

    private void Start()
    {
        // Reset the pad states at the start of each level
        player1OnPad = false;
        player2OnPad = false;

        gameManager = FindObjectOfType<GameManager>();

        // Add or configure an AudioSource on this GameObject
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = transitionSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == playerName)
        {
            if (playerName == "Player1" && !player1OnPad)
            {
                TriggerBeaconFires(beaconControllerBlue);
                player1OnPad = true;
                Debug.Log("Player 1 is on their pad.");
                CheckTransitionCondition();
            }
            else if (playerName == "Player2" && !player2OnPad)
            {
                TriggerBeaconFires(beaconControllerRed);
                player2OnPad = true;
                Debug.Log("Player 2 is on their pad.");
                CheckTransitionCondition();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == playerName)
        {
            if (playerName == "Player1")
            {
                PutOutBeaconFires(beaconControllerBlue);
                player1OnPad = false;
                Debug.Log("Player 1 left their pad.");
            }
            else if (playerName == "Player2")
            {
                PutOutBeaconFires(beaconControllerRed);
                player2OnPad = false;
                Debug.Log("Player 2 left their pad.");
            }
        }
    }

    private void CheckTransitionCondition()
    {
        if (singlePlayerMode)
        {
            if ((playerName == "Player1" && player1OnPad) || (playerName == "Player2" && player2OnPad))
            {
                HandleLevelTransition();
            }
        }
        else if (player1OnPad && player2OnPad)
        {
            HandleLevelTransition();
        }
    }

    private void HandleLevelTransition()
    {
        PersistentMenu.instance.inTransit = true;
        if (audioSource != null && transitionSound != null)
        {
            audioSource.Play();
            StartCoroutine(WaitForSoundAndTransition());
        }
        else
        {
            Debug.LogWarning("Transition sound is missing!");
            PerformTransition();
        }
    }

    private IEnumerator WaitForSoundAndTransition()
    {
        yield return new WaitForSeconds(transitionSound.length - 2); // Wait for the sound to finish
        PerformTransition();
    }

    private void PerformTransition()
    {
        Debug.Log("Transitioning to the next scene.");
        if (gameManager != null)
        {
            gameManager.TriggerCompleteLevel();
        }
    }

    private void TriggerBeaconFires(BeaconController beaconController)
    {
        if (beaconController != null)
        {
            beaconController.LightUp();
        }
        else
        {
            Debug.LogWarning("BeaconController reference is not set!");
        }
    }

    private void PutOutBeaconFires(BeaconController beaconController)
    {
        if (beaconController != null)
        {
            beaconController.LightDown();
        }
        else
        {
            Debug.LogWarning("BeaconController reference is not set!");
        }
    }
}
