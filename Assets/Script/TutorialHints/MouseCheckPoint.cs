using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MouseCheckPoint : MonoBehaviour
{
    public float movementThreshold = 10.0f; // Minimum distance required to trigger detection
    private float accumulatedMovement = 0.0f;

    public Image firstCheckpointImage;
    public Image checkpointImage;
    public Image lastpointImage;
    public float fadeDuration = 2.0f;
    private bool hasTriggered;
    private bool hasMovedMouse;

    private CameraRotationTutorialTracker cameraRotationTutorialTracker;
    private PlayerMoveTutorialTracker playerMoveTutorialTracker;

    private void Start()
    {
        cameraRotationTutorialTracker = FindObjectOfType<CameraRotationTutorialTracker>();
        playerMoveTutorialTracker = FindObjectOfType<PlayerMoveTutorialTracker>();

        firstCheckpointImage.gameObject.SetActive(false);
        checkpointImage.gameObject.SetActive(false);
        lastpointImage.gameObject.SetActive(false);
        hasTriggered = false;
        hasMovedMouse = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Calculate movement magnitude
        float movement = Mathf.Sqrt(mouseX * mouseX + mouseY * mouseY);
        accumulatedMovement += movement;

        // Check if accumulated movement exceeds the threshold
        if (accumulatedMovement >= movementThreshold && !hasMovedMouse)
        {
            hasMovedMouse = true;
            ShowSecondCheckpointUI();
            SendCameraRotationEvent();

            // Reset accumulated movement if needed for repeated detection
            accumulatedMovement = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            Debug.Log("Player entered mouse checkpoint");
            ShowCheckpointUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasMovedMouse)
        {
            hasMovedMouse = true;
            firstCheckpointImage.gameObject.SetActive(false);
            StartCoroutine(ShowLastCehckpointUI());
        }
    }

    private void ShowCheckpointUI()
    {
        firstCheckpointImage.gameObject.SetActive(true);
    }


    private void ShowSecondCheckpointUI()
    {
        firstCheckpointImage.gameObject.SetActive(false);

        checkpointImage.gameObject.SetActive(true);

        StartCoroutine(ShowLastCehckpointUI());
    }

    private IEnumerator ShowLastCehckpointUI()
    {
        yield return new WaitForSeconds(1f);
        checkpointImage.gameObject.SetActive(false);

        lastpointImage.gameObject.SetActive(true);
        StartPlayerMoveEvent();
    }

    private void SendCameraRotationEvent()
    {
        if (cameraRotationTutorialTracker != null)
        {
            cameraRotationTutorialTracker.SendEvent();
        }
    }

    private void StartPlayerMoveEvent()
    {
        if (playerMoveTutorialTracker != null)
        {
            playerMoveTutorialTracker.StartTracking();
        }
    }
}