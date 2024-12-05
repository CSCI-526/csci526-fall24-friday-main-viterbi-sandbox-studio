using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckpointHideTwo : MonoBehaviour
{
    public Image checkpointImage;          // The first checkpoint UI image
    public Image secondCheckpointImage;   // The second checkpoint UI image
    public float fadeDuration = 3.0f;     // Duration of fade-out effect
    public AudioClip checkpointSound;     // Sound effect for checkpoint trigger

    private AudioSource audioSource;      // Audio source to play the sound
    private bool hasTriggered;            // Flag to prevent multiple triggers

    public void Start()
    {
        secondCheckpointImage.gameObject.SetActive(false);
        hasTriggered = false;

        // Add or use an existing AudioSource on the GameObject
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = checkpointSound;
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            Debug.Log("Player entered checkpointHide");

            // Play the sound effect
            if (checkpointSound != null)
            {
                audioSource.Play();
            }

            HideCheckpointUI();
        }
    }

    private void HideCheckpointUI()
    {
        if (checkpointImage != null)
        {
            checkpointImage.gameObject.SetActive(false); // Hide the first image
        }

        secondCheckpointImage.gameObject.SetActive(true); // Show the second image

        StartCoroutine(FadeOutUI()); // Start fade-out coroutine
    }

    private IEnumerator FadeOutUI()
    {
        Color originalColor = secondCheckpointImage.color;
        float startAlphaImage = secondCheckpointImage.color.a;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;

            // Gradually reduce the alpha of the second checkpoint image
            Color imageColor = secondCheckpointImage.color;
            imageColor.a = Mathf.Lerp(startAlphaImage, 0, normalizedTime);
            secondCheckpointImage.color = imageColor;

            yield return null; // Wait for the next frame
        }

        secondCheckpointImage.gameObject.SetActive(false); // Hide the second image

        // Restore the original color
        originalColor.a = startAlphaImage;
        secondCheckpointImage.color = originalColor;
    }
}
