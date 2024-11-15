using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PersistentMenu : MonoBehaviour
{
    public static PersistentMenu instance;
    public GameObject mainPanel; 
    public GameObject LevelPanel;
    public GameObject winContext;
    public GameObject MainText;
    public GameObject menuButton;
    public GameObject winEndGame;
    public TMP_Text levelTitle;

    public bool inTransit = false;
    public bool menuOpened = false; // disable input

    private LevelManager levelManager;

    private void Awake()
    {
        // Ensure only one instance of the canvas exists across scenes
        levelManager = FindObjectOfType<LevelManager>();
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Make this canvas persistent

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if (inTransit) return;

        // ESC open menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mainPanel.activeSelf)
            {
                HideMainMenu();
            }
            else
            {
                ShowMainMenu();
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenuScene")
        {
            levelTitle.gameObject.SetActive(false);
            ShowMainMenu();
        }
        else
        {   
            levelTitle.text = levelManager.GetCurrentLevelName();
            levelTitle.gameObject.SetActive(true);
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

    public void ShowWinEnd() {
        winContext.SetActive(false);
        MainText.SetActive(false);
        winEndGame.SetActive(true);
    }

    public void HideWinEnd() {
        MainText.SetActive(true);
        winEndGame.SetActive(false);
        winContext.SetActive(false);
    }

    public void ShowMainMenu()
    {
        menuOpened = true;
        menuButton.SetActive(false);
        LevelPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void HideMainMenu()
    {
        menuOpened = false;
        HideWinEnd();
        menuButton.SetActive(true);
        mainPanel.SetActive(false);
        LevelPanel.SetActive(false);
    }

    public void WinTransit()
    {
        if (levelManager.GetLevel() == 6)
        {
            ShowWinEnd();
        }
        else
        {
            showWinContext();
        }
        ShowMainMenu();
        inTransit = true;
    }

    public bool IsInTransitOrMenuOpened()
    {
        return inTransit || menuOpened;
    }
}
