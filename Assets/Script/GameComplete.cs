using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameComplete : MonoBehaviour
{
    public TextMeshProUGUI winText;
    public GameObject winPanel;

    private int reached = 0;
    private HashSet<GameObject> reachedPlayers = new HashSet<GameObject>();

    void Start()
    {
        
        winPanel.SetActive(false);
        winText.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !reachedPlayers.Contains(other.gameObject))
        {
            reached++;
            reachedPlayers.Add(other.gameObject);

            if (reached == 2)
            {
                winPanel.SetActive(true);
                winText.enabled = true;
            }
        }

    }


}
