using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckpointHideTwo : MonoBehaviour
{
    public Image checkpointImage;    
    public Text checkpointText;       
    public Image secondCheckpointImage; 
    public Text secondCheckpointText; 
    public float fadeDuration = 3.0f;   

    public void Start()
    {
        secondCheckpointImage.gameObject.SetActive(false);
        secondCheckpointText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered checkpointHide");
            HideCheckpointUI();
        }
    }

    private void HideCheckpointUI()
    {
        checkpointImage.gameObject.SetActive(false);
        checkpointText.gameObject.SetActive(false);
        
        secondCheckpointImage.gameObject.SetActive(true);
        secondCheckpointText.gameObject.SetActive(true);
        
        StartCoroutine(FadeOutUI());
    }

    private IEnumerator FadeOutUI()
    {
        float startAlphaImage = secondCheckpointImage.color.a;
        float startAlphaText = secondCheckpointText.color.a;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration; 
            
            Color imageColor = secondCheckpointImage.color;
            imageColor.a = Mathf.Lerp(startAlphaImage, 0, normalizedTime);
            secondCheckpointImage.color = imageColor;
            
            Color textColor = secondCheckpointText.color;
            textColor.a = Mathf.Lerp(startAlphaText, 0, normalizedTime);
            secondCheckpointText.color = textColor;

            yield return null; 
        }
        
        secondCheckpointImage.gameObject.SetActive(false);
        secondCheckpointText.gameObject.SetActive(false);
    }
}