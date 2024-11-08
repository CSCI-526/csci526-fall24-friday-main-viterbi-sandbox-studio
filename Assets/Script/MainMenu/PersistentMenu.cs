using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentMenu : MonoBehaviour
{
    public static PersistentMenu instance;
    public GameObject mainPanel; 
    public GameObject LevelPanel;
    public GameObject winContext;
    public GameObject MainText;


    private void Awake()
    {
        // Ensure only one instance of the canvas exists across scenes
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Make this canvas persistent

        // Subscribe to scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenuScene")
        {
            ShowMainMenu();
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the scene loaded event when the object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void showWinContext()
    {
        winContext.SetActive(true);
        MainText.SetActive(false);
    }

    public void noShowWinContext()
    {
        winContext.SetActive(false);
        MainText.SetActive(true);
    }

    public void ShowMainMenu()
    {
        LevelPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
