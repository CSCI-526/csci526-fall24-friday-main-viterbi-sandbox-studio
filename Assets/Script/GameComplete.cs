using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameComplete : MonoBehaviour
{
    public TextMeshProUGUI winText;
    private int reached = 0;
    private HashSet<GameObject> reachedPlayers = new HashSet<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !reachedPlayers.Contains(other.gameObject))
        {
            reached++;
            reachedPlayers.Add(other.gameObject);

            if (reached == 2)
            {
                winText.enabled = true;
            }
        }

    }


}
