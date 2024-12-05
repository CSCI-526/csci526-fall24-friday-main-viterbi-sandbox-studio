using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOutOfBounds : MonoBehaviour
{
    private LevelResetManager levelResetManager;
    private SceneTransitionManager sceneTransitionManager;

    [Tooltip("Sound effect to play when the game resets.")]
    public AudioClip resetSound; // Sound effect for game reset
    private AudioSource audioSource; // Audio source to play the sound

    void Start()
    {
        levelResetManager = FindObjectOfType<LevelResetManager>();
        sceneTransitionManager = FindObjectOfType<SceneTransitionManager>();

        // Add or configure an AudioSource on this GameObject
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = resetSound;

        // Debug logs to verify setup
        if (audioSource != null)
        {
            Debug.Log("AudioSource successfully added.");
        }
        else
        {
            Debug.LogError("Failed to add AudioSource to the GameObject.");
        }

        if (resetSound != null)
        {
            Debug.Log("Reset sound effect is assigned.");
        }
        else
        {
            Debug.LogWarning("Reset sound effect is not assigned in the Inspector!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!PersistentMenu.instance.inTransit && ShouldTriggerReset(other))
        {
            StartCoroutine(ResetGame());
        }
    }

    private bool ShouldTriggerReset(Collider other)
    {
        return other.CompareTag("Player") ||
            other.CompareTag("BoxLevelF");
    }

    private IEnumerator ResetGame()
    {
        // Play reset sound effect
        if (audioSource != null && resetSound != null)
        {
            audioSource.Play();
            Debug.Log("Reset sound effect played.");
        }
        else
        {
            Debug.LogWarning("Reset sound effect or AudioSource is not set!");
        }

        PersistentMenu.instance.inTransit = true;
        yield return StartCoroutine(sceneTransitionManager.FadeOut());
        levelResetManager.ResetCurrentLevelObjects();
        yield return new WaitForSeconds(1f);
        PersistentMenu.instance.inTransit = false;
        yield return StartCoroutine(sceneTransitionManager.FadeIn());
    }
}
