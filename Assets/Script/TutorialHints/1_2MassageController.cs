using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SecondMessageController : MonoBehaviour
{
    public Text secondMessage;
    public Image secondBackground; 
    public Transform boxTransform; 
    private Vector3 initialBoxPosition; 
    private float moveThreshold = 1.0f; 
    private float fadeDuration = 3.0f; 
    private bool hasTriggered = false; 

    private void Start()
    {
        secondMessage.gameObject.SetActive(false);
        secondBackground.gameObject.SetActive(false);
        
        if (boxTransform != null)
        {
            initialBoxPosition = boxTransform.position;
        }
    }

    private void Update()
    {
        if (!hasTriggered && boxTransform != null && Vector3.Distance(boxTransform.position, initialBoxPosition) >= moveThreshold)
        {
            hasTriggered = true;
            StartCoroutine(ShowAndFadeOutSecondMessage());
        }
    }

    private IEnumerator ShowAndFadeOutSecondMessage()
    {
        yield return new WaitForSeconds(0.5f);
        
        secondMessage.gameObject.SetActive(true);
        secondBackground.gameObject.SetActive(true);
        
        CanvasGroup messageCanvasGroup = secondMessage.GetComponent<CanvasGroup>();
        CanvasGroup backgroundCanvasGroup = secondBackground.GetComponent<CanvasGroup>();
        
        if (messageCanvasGroup == null)
        {
            messageCanvasGroup = secondMessage.gameObject.AddComponent<CanvasGroup>();
        }
        if (backgroundCanvasGroup == null)
        {
            backgroundCanvasGroup = secondBackground.gameObject.AddComponent<CanvasGroup>();
        }
        
        messageCanvasGroup.alpha = 1;
        backgroundCanvasGroup.alpha = 1;
        
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            messageCanvasGroup.alpha = alpha;
            backgroundCanvasGroup.alpha = alpha;
            yield return null;
        }
        
        secondMessage.gameObject.SetActive(false);
        secondBackground.gameObject.SetActive(false);
    }
}