using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetGame : MonoBehaviour
{
    public Button playButton;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(ResetGameScene);

        gameManager = FindObjectOfType<GameManager>();
    }

    // Method to reset the game
    public void ResetGameScene()
    {
        // Reload the current scene
        gameManager.ReloadScene();
        PersistentMenu.instance.inTransit = false;
        PersistentMenu.instance.HideMainMenu();
    }
}
