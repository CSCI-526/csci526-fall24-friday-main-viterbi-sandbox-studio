using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckpointHideTwo : MonoBehaviour
{
    public Image checkpointImage;       
    public Image secondCheckpointImage; 
    public float fadeDuration = 3.0f;
    private bool hasTriggered;

    public void Start()
    {
        secondCheckpointImage.gameObject.SetActive(false);
        hasTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            Debug.Log("Player entered checkpointHide");
            HideCheckpointUI();
        }
    }

    private void HideCheckpointUI()
    {
        checkpointImage.gameObject.SetActive(false);
        
        secondCheckpointImage.gameObject.SetActive(true);
        
        StartCoroutine(FadeOutUI());
    }

    private IEnumerator FadeOutUI()
    {
        Color originalColor = secondCheckpointImage.color;
        float startAlphaImage = secondCheckpointImage.color.a;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration; 
            
            Color imageColor = secondCheckpointImage.color;
            imageColor.a = Mathf.Lerp(startAlphaImage, 0, normalizedTime);
            secondCheckpointImage.color = imageColor;

            yield return null; 
        }
        
        secondCheckpointImage.gameObject.SetActive(false);
        // Restore the original color
        originalColor.a = startAlphaImage;
        secondCheckpointImage.color = originalColor;
    }
}