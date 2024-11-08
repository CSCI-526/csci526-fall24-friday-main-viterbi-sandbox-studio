using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text firstMessage;
    public Image firstBackground; 
    public Text secondMessage; 
    public Image secondBackground; 
    private float fadeDuration = 5.0f; 
    private bool isShowingFirstUI = true; 
    private bool isFading = false; 
    private bool hasShownSecondUI = false; 

    private void Start()
    {
        firstMessage.gameObject.SetActive(true);
        firstBackground.gameObject.SetActive(true);
        
        secondMessage.gameObject.SetActive(false);
        secondBackground.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isFading)
        {
            if (isShowingFirstUI && !hasShownSecondUI)
            {
                firstMessage.gameObject.SetActive(false);
                firstBackground.gameObject.SetActive(false);
                StartCoroutine(ShowAndFadeOutSecondUI());
            }
            else if (!hasShownSecondUI)
            {
                firstMessage.gameObject.SetActive(true);
                firstBackground.gameObject.SetActive(true);
            }
            isShowingFirstUI = !isShowingFirstUI; 
        }
    }

    private IEnumerator ShowAndFadeOutSecondUI()
    {
        secondMessage.gameObject.SetActive(true);
        secondBackground.gameObject.SetActive(true);
        
        isFading = true; 
        hasShownSecondUI = true; 
        
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
        isFading = false; 
    }
}
