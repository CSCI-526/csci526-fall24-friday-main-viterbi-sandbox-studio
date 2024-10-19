using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Vector3 initialOffset = new Vector3(-7, 5, 0); 
    private Vector3 closeOffset = new Vector3(0.5f, 0, 0); 
    private Vector3 initialRotation = new Vector3(25, 90, 0); 
    private Vector3 closeRotation = new Vector3(0, 90, 0); 
    private float zoomSpeed = 7f; 
    private float maxDistance = 7f;
    public LayerMask wallLayerMask; // Layer mask to detect walls

    private Vector3 currentOffset;
    private Vector3 currentRotation;

    private CharacterSwitcher characterSwitcher;

    void Start()
    {
        characterSwitcher = FindObjectOfType<CharacterSwitcher>();
        GameObject activePlayer = characterSwitcher.getActivePlayer();
        currentOffset = initialOffset; // Start with the initial offset
        currentRotation = initialRotation; // Start with the initial rotation
        transform.position = activePlayer.transform.position + currentOffset;
        transform.rotation = Quaternion.Euler(currentRotation);
    }

    void LateUpdate()
    {
        // check if there's a wall behind
        GameObject activePlayer = characterSwitcher.getActivePlayer();
        RaycastHit hit;
        float t = 0f;
        bool isWallHit = false; 
        if (Physics.Raycast(activePlayer.transform.position, -transform.forward, out hit, maxDistance, wallLayerMask))
        {
            // Calculate the interpolation factor 't' based on the distance to the wall
            // 't' will be 0 when far from the wall, and 1 when right next to it
            t = 1 - (hit.distance / maxDistance);
            isWallHit = true;
        }

        if (isWallHit)
        {
            // Smoothly transition
            currentOffset = Vector3.Lerp(initialOffset, closeOffset, t);
            currentRotation = Vector3.Lerp(initialRotation, closeRotation, t);
        }
        else
        {
            currentOffset = initialOffset;
            currentRotation = initialRotation;
        }

        transform.position = activePlayer.transform.position + currentOffset;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(currentRotation), Time.deltaTime * zoomSpeed);
    }
}
