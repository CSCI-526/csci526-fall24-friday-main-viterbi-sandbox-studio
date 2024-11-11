using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    // Function to start the game by loading the first level
    public Button startGameButton;
    public Button menuButton;
    public Button continueButton;
    public Button levelButton;
    public Button restartButton;

    public GameObject mainPanel;
    public GameObject LevelPanel;

    public Button l1Button;
    public Button l2Button;
    public Button l3Button;
    public Button l4Button;
    public Button backButton;

    private GameManager gameManager;

    private void Start()
    {
        // Assign the StartGame function to the button's onClick event
        startGameButton.onClick.AddListener(StartGame);
        menuButton.onClick.AddListener(ShowMainMenu);
        continueButton.onClick.AddListener(ContinueGame);
        levelButton.onClick.AddListener(ShowLevelMenu);
        l1Button.onClick.AddListener(() => LoadTutorialLevel(1));
        l2Button.onClick.AddListener(() => LoadTutorialLevel(2));
        l3Button.onClick.AddListener(() => LoadTutorialLevel(3));
        l4Button.onClick.AddListener(LoadLevel1);
        backButton.onClick.AddListener(ShowMainMenu);

        gameManager = FindObjectOfType<GameManager>();
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene("tutortiallevel1"); 
        InactiveMenu();
        ChangeButtonPattern();
    }

    public void ChangeButtonPattern()
    {
        startGameButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void ShowMainMenu()
    {
        LevelPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void ContinueGame()
    {
        mainPanel.SetActive(false);
        PersistentMenu.instance.noShowWinContext();
    }

    public void ShowLevelMenu()
    {
        mainPanel.SetActive(false);
        LevelPanel.SetActive(true);
    }

    public void InactiveMenu()
    {
        mainPanel.SetActive(false);
        LevelPanel.SetActive(false);
    }

    public void LoadTutorialLevel(int levelNumber)
    {
        // Load tutorial levels based on the selected number (1, 2, or 3)
        bool result = gameManager.StartLevel(levelNumber);
        if (!result)
        {
            Debug.LogError("Invalid tutorial level number");
            return;
        }
        InactiveMenu();
        ChangeButtonPattern();
        PersistentMenu.instance.noShowWinContext();
    }

    public void LoadLevel1()
    {
        int levelNumber = 4;
        bool result = gameManager.StartLevel(levelNumber);
        if (!result)
        {
            Debug.LogError("Invalid level number");
            return;
        }
        InactiveMenu();
        ChangeButtonPattern();
        PersistentMenu.instance.noShowWinContext();
    }

}
