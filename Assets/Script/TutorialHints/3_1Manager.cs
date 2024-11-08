using UnityEngine;
using UnityEngine.UI;

public class TFirstMessageController : MonoBehaviour
{
    public Text firstMessage; 
    public Image firstBackground; 
    public Transform boxTransform; 
    private Vector3 initialBoxPosition;
    private float moveThreshold = 1.0f; 

    private void Start()
    {
        
        firstMessage.gameObject.SetActive(false);
        firstBackground.gameObject.SetActive(false);
        
        if (boxTransform != null)
        {
            initialBoxPosition = boxTransform.position;
        }

        Invoke("ShowFirstMessage", 12f);
    }

    private void ShowFirstMessage()
    {
        firstMessage.gameObject.SetActive(true);
        firstBackground.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (boxTransform != null && Vector3.Distance(boxTransform.position, initialBoxPosition) >= moveThreshold)
        {
            firstMessage.gameObject.SetActive(false);
            firstBackground.gameObject.SetActive(false);
        }
    }
}