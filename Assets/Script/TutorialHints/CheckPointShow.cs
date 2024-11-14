using UnityEngine;
using UnityEngine.UI;

public class CheckpointShow : MonoBehaviour
{
    public Image checkpointImage;  
    public Text checkpointText;     

    private void Start()
    {
        checkpointImage.gameObject.SetActive(false);
        checkpointText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered checkpointShow");
            ShowCheckpointUI();
        }
    }

    private void ShowCheckpointUI()
    {
        checkpointImage.gameObject.SetActive(true);
        checkpointText.gameObject.SetActive(true);
    }
}