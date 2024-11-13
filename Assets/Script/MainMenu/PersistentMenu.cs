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
    public GameObject menuButton;

    public bool inTransit = false;
    public bool disableInput = false;

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

    public void HideWinContext()
    {
        winContext.SetActive(false);
        MainText.SetActive(true);
    }

    public void ShowMainMenu()
    {
        menuButton.SetActive(false);
        LevelPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void HideMainMenu()
    {
        menuButton.SetActive(true);
        mainPanel.SetActive(false);
        LevelPanel.SetActive(false);
    }

    public void WinTransit()
    {
        showWinContext();
        ShowMainMenu();
        inTransit = true;
    }
}
