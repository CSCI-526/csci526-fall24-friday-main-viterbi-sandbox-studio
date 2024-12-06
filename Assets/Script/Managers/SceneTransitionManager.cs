using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public Image fadeImage;
    public float fadeOutDelay = 1f;
    public float fadeOutDuration = 1f;
    public float fadeInDuration = 1f;

    private void Awake()
    {
        // Ensure the image starts fully transparent
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0, 0, 0, 1);
            fadeImage.enabled = false;
        }
            
    }

    public IEnumerator FadeOutDelay()
    {
        // Delay before fade-out
        yield return new WaitForSeconds(fadeOutDelay);
    }

    public IEnumerator FadeOut()
    {
        // Fade-out (screen blacking out)
        
        yield return StartCoroutine(Fade(0, 1, fadeOutDuration));
    }

    public IEnumerator FadeIn()
    {
        // Fade-in (from black to visible)
        yield return StartCoroutine(Fade(1, 0, fadeInDuration));
        
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        fadeImage.enabled = true;
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Ensure the final alpha is set
        fadeImage.color = new Color(color.r, color.g, color.b, endAlpha);
        if (endAlpha == 0)
        {
            fadeImage.enabled = false;
        }
    }
}
