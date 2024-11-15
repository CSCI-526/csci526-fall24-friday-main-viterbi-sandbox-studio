using UnityEngine;
using UnityEngine.UI;

public class CheckpointShow : MonoBehaviour
{
    public Image prevCheckpointImage;
    public Image checkpointImage;
    private bool hasTriggered;  

    private void Start()
    {
        if (prevCheckpointImage != null)
        {
            prevCheckpointImage.gameObject.SetActive(false);
        }
        checkpointImage.gameObject.SetActive(false);
        hasTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            Debug.Log("Player entered checkpointShow");
            ShowCheckpointUI();
        }
    }

    private void ShowCheckpointUI()
    {
        if (prevCheckpointImage != null)
        {
            prevCheckpointImage.gameObject.SetActive(false);
        }
        checkpointImage.gameObject.SetActive(true);
    }
}