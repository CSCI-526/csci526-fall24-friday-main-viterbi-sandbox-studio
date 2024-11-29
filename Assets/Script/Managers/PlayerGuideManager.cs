using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuideManager : MonoBehaviour
{
    [Tooltip("List of positions for this level. Configure in the Inspector.")]
    public List<Vector3> positions; // The list of positions the object will move to.

    private int currentPositionIndex = 0; // Tracks the current position index.

    private void Start()
    {
        transform.position = positions[currentPositionIndex];
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
