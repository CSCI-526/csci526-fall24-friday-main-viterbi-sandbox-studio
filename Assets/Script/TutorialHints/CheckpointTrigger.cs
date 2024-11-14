using UnityEngine;
using UnityEngine.UI;

public class CheckpointDisplay : MonoBehaviour
{
    public Image checkpointImage;   
    public Text checkpointText;     
    public GameObject checkpoint;    
    private void Start()
    {
        checkpointImage.gameObject.SetActive(false);
        checkpointText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("Player entered checkpoint trigger");
            ShowCheckpointUI();
            
            Invoke("HideCheckpoint", 3f);
        }
    }

    private void ShowCheckpointUI()
    {
        checkpointImage.gameObject.SetActive(true);
        checkpointText.gameObject.SetActive(true);
        
    }

    private void HideCheckpointUI()
    {
        checkpointImage.gameObject.SetActive(false);
        checkpointText.gameObject.SetActive(false);
    }

    private void HideCheckpoint()
    {
        checkpoint.SetActive(false); 
    }
}