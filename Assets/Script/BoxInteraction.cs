using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoxInteraction : MonoBehaviour
{
    [SerializeField] private Text hintText;  // Hint text UI element
    [SerializeField] private float displayDelay = 1f;  // Delay before showing the hint after collision
    [SerializeField] private float displayDuration = 5f;  // Duration for which the hint will be visible
    private bool hasShownHint = false;  // Tracks if the hint has been shown

    void Start()
    {
        // Make sure the hint text is hidden at the start
        if (hintText != null)
        {
            hintText.enabled = false;
        }
        else
        {
            Debug.LogError("Hint Text UI is not assigned in the Inspector!");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasShownHint)
        {
            StartCoroutine(ShowHintWithDelay());
        }
    }

    IEnumerator ShowHintWithDelay()
    {
        hasShownHint = true;
        
        yield return new WaitForSeconds(displayDelay);

        if (hintText != null)
        {
            hintText.text = "You need two players to push this box together!";
            hintText.enabled = true;  // Display the hint text
        }

        // Wait for the specified duration before hiding the hint
        yield return new WaitForSeconds(displayDuration);

        if (hintText != null)
        {
            hintText.enabled = false;  // Hide the hint text
        }
    }
}