using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MouseCheckPoint : MonoBehaviour
{
    private float movementThreshold = 150.0f; // Minimum distance required to trigger detection
    private float accumulatedMovement = 0.0f;

    public Image firstCheckpointImage;
    public Image checkpointImage;
    public Image lastpointImage;
    public float fadeDuration = 3.0f;
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
        if (!hasTriggered)
        {
            return;
        }

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
            Debug.Log("Player entered mouse checkpoint");
            StartCoroutine(ShowCheckpointUI());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasMovedMouse)
        {
            hasMovedMouse = true;
            firstCheckpointImage.gameObject.SetActive(false);
            checkpointImage.gameObject.SetActive(false);
            StartCoroutine(ShowLastCehckpointUI());
        }
    }

    private IEnumerator ShowCheckpointUI()
    {
        yield return new WaitForSeconds(1f);
        hasTriggered = true;
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
        StartCoroutine(FadeOutUI());

        StartPlayerMoveEvent();
    }

    private IEnumerator FadeOutUI()
    {
        float startAlphaImage = lastpointImage.color.a;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            //float normalizedTime = t / fadeDuration;

            //Color imageColor = lastpointImage.color;
            //imageColor.a = Mathf.Lerp(startAlphaImage, 0, normalizedTime);
            //lastpointImage.color = imageColor;

            yield return null;
        }

        lastpointImage.gameObject.SetActive(false);
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