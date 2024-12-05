using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuideManager : MonoBehaviour
{
    [Tooltip("List of positions for this level. Configure in the Inspector.")]
    public List<Vector3> positions = new List<Vector3>(); // The list of positions the object will move to.

    [Tooltip("Sound to play when moving to the next position.")]
    public AudioClip moveSound; // The sound effect to play when moving.

    private AudioSource audioSource; // Audio source to play the sound.
    private int currentPositionIndex = 0; // Tracks the current position index.

    private void Start()
    {
        // Check if the positions list is populated.
        if (positions.Count > 0)
        {
            // Set the initial position.
            transform.position = positions[currentPositionIndex];
        }
        else
        {
            Debug.Log("Positions list is empty. Please add positions in the Inspector.");
        }

        // Add or configure an AudioSource on this GameObject.
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = moveSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player stepped on the object.
        if (other.CompareTag("Player") && currentPositionIndex < positions.Count - 1)
        {
            MoveToNextPosition();
        }
    }

    private void MoveToNextPosition()
    {
        // Increment the position index and update the object's position.
        currentPositionIndex++;
        transform.position = positions[currentPositionIndex];

        // Play the move sound if it's set.
        if (audioSource != null && moveSound != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Move sound is missing or AudioSource is not set!");
        }
    }
}
