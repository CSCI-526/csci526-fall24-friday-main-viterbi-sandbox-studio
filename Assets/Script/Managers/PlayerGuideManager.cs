using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuideManager : MonoBehaviour
{
    [Tooltip("List of positions for this level. Configure in the Inspector.")]
    public List<Vector3> positions = new List<Vector3>(); // The list of positions the object will move to.

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
            Debug.LogError("Positions list is empty. Please add positions in the Inspector.");
        }
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
    }
}
