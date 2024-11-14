using UnityEngine;
using UnityEngine.UI;

public class CheckpointHide : MonoBehaviour
{
    public Image checkpointImage;   
    public Text checkpointText;      

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
    }
}