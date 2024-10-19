using UnityEngine;
using UnityEngine.UI;
using System.Collections;  // Required for coroutines

public class PlayerTips : MonoBehaviour
{
    [SerializeField] private Text promptText;  // Initial prompt text
    [SerializeField] private Text promptTextNext;  // Success prompt text after switching
    [SerializeField] private float fadeDuration = 5f;  // Fade-out duration for the success text
    [SerializeField] private int stepsToShowPrompt = 10;  // Steps required to show the prompt
    private Vector3 lastPosition;
    private int stepCount = 0;
    private bool promptShown = false;
    private bool hasSwitched = false;  

    void Start()
    {
        lastPosition = transform.position;

        if (promptText != null)
        {
            promptText.enabled = false;  
        }
        else
        {
            Debug.LogError("Prompt Text UI is not assigned in the Inspector!");
        }

        if (promptTextNext != null)
        {
            promptTextNext.enabled = false;  
        }
        else
        {
            Debug.LogError("Next Prompt Text UI is not assigned in the Inspector!");
        }
    }

    void Update()
    {
        if (hasSwitched) return;

        // Track steps based on player movement
        float distance = Vector3.Distance(transform.position, lastPosition);
        if (distance > 0.1f)  
        {
            stepCount++;
            lastPosition = transform.position;
        }
        
        if (stepCount >= stepsToShowPrompt && !promptShown)
        {
            ShowPrompt();
        }
        
        if (promptShown && Input.GetKeyDown(KeyCode.R))
        {
            HidePromptAndShowNext();
        }
    }

    void ShowPrompt()
    {
        if (promptText != null)
        {
            promptText.text = "Press R to switch. Try it out!";
            promptText.enabled = true;  // Display the prompt
            promptShown = true;
        }
    }

    void HidePromptAndShowNext()
    {
        if (promptText != null)
        {
            promptText.enabled = false;  // Hide the initial prompt
        }

        if (promptTextNext != null)
        {
            promptTextNext.text = "You did it! You're awesome!";  
            promptTextNext.enabled = true;  // Show success message
            StartCoroutine(FadeOutText(promptTextNext));  // Start fading the success message
        }

        promptShown = false;
        hasSwitched = true;  // Mark as switched to avoid repeated prompts
    }

    IEnumerator FadeOutText(Text text)
    {
        float elapsedTime = 0f;
        Color originalColor = text.color;

        // Gradually reduce text opacity over time
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;  // Wait for the next frame
        }

        text.enabled = false;  // Hide text after fading out
    }
}
