using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTrigger : MonoBehaviour, IResettable
{
    public GameObject tile;             
    public GameObject nextTile;         
    public GameObject nextTileTrigger;  
    private int playersOnTile = 0;

    private int _initialPlayersOnTile = 0;
    public bool initialActive = true;

    public void SaveInitialState()
    {
        _initialPlayersOnTile = playersOnTile;
        tile.SetActive(initialActive);
        gameObject.SetActive(initialActive);
    }

    public void ResetState()
    {
        playersOnTile = _initialPlayersOnTile;
        tile.SetActive(initialActive);
        gameObject.SetActive(initialActive);
    }

    void Start()
    {
        SaveInitialState();
        FindObjectOfType<LevelResetManager>().RegisterObject(this);
    }

    private void OnEnable()
    {
        playersOnTile = 0;
        Debug.Log(gameObject.name + " OnEnable: playersOnTile reset to 0");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playersOnTile++;
            Debug.Log("Player entered: " + gameObject.name + ", playersOnTile: " + playersOnTile);

            // Activate next tile and trigger only if both players are on the tile
            if (playersOnTile == 2)  // Both players are on the tile
            {
                if (nextTile != null)
                {
                    nextTile.SetActive(true);
                    Debug.Log(nextTile.name + " activated");
                }
                if (nextTileTrigger != null)
                {
                    nextTileTrigger.SetActive(true);
                    Debug.Log(nextTileTrigger.name + " trigger activated");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playersOnTile--;
            Debug.Log("Player exited: " + gameObject.name + ", playersOnTile: " + playersOnTile);

            // If there is no next tile and both players have left the current tile, deactivate it
            if (playersOnTile <= 0 && nextTile == null)
            {
                StartCoroutine(DeactivateAfterDelay(tile, 0.1f));
                StartCoroutine(DeactivateAfterDelay(gameObject, 0.1f));
                Debug.Log(tile.name + " deactivated because it is the last tile and both players have left.");
            }
        }
    }

    private void LateUpdate()
    {
        // Check if both players are on the next tile trigger
        if (nextTileTrigger != null)
        {
            TileTrigger nextTileTriggerScript = nextTileTrigger.GetComponent<TileTrigger>();
            if (nextTileTriggerScript != null && nextTileTriggerScript.playersOnTile == 2)
            {
                StartCoroutine(DeactivateAfterDelay(tile, 0.1f));
                StartCoroutine(DeactivateAfterDelay(gameObject, 0.1f));
                Debug.Log(tile.name + " deactivated because both players are on the next tile's trigger.");
            }
        }
    }

    private IEnumerator DeactivateAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
        Debug.Log(obj.name + " deactivated after delay");
    }

    public void ResetPlayersOnTile()
    {
        playersOnTile = 0;
        Debug.Log(gameObject.name + " playersOnTile reset to 0 by ResetPlayersPosition");
    }
}
