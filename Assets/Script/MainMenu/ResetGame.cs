using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetGame : MonoBehaviour
{
    public Button playButton;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(ResetGameScene);
    }

    // Method to reset the game
    public void ResetGameScene()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
