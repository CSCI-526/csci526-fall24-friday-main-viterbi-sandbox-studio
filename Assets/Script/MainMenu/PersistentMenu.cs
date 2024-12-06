using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PersistentMenu : MonoBehaviour
{
    public static PersistentMenu instance;
    public GameObject mainPanel; 
    public GameObject LevelPanel;
    public GameObject ControlPanel;
    public GameObject menuButton;
    public GameObject winEndGame;
    public TMP_Text levelTitle;
    public GameObject levelBlock;

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
        if (Input.GetKeyDown(KeyCode.M))
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
            levelBlock.SetActive(false);
            ShowMainMenu();
        }
        else
        {   
            levelTitle.text = levelManager.GetCurrentLevelName();
            levelBlock.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the scene loaded event when the object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void showWinContext()
    {

    }

    public void ShowWinEnd() {
        levelBlock.SetActive(false);
        menuButton.gameObject.SetActive(false);
        winEndGame.SetActive(true);
    }

    public void HideWinEnd() {

        winEndGame.SetActive(false);
    }

    public void ShowMainMenu()
    {
        // When the game is over, show the cursor and unlock it
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        menuOpened = true;
        menuButton.SetActive(false);
        LevelPanel.SetActive(false);
        mainPanel.SetActive(true);
        ControlPanel.SetActive(false);
        HideWinEnd();
    }

    public void HideMainMenu()
    {
        // Hide the cursor and lock it to the center of the screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        menuOpened = false;
        HideWinEnd();
        menuButton.SetActive(true);
        mainPanel.SetActive(false);
        LevelPanel.SetActive(false);
        ControlPanel.SetActive(false);
    }

    public bool IsInTransitOrMenuOpened()
    {
        return inTransit || menuOpened;
    }
}
