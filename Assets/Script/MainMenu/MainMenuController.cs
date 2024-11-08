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
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene("tutortiallevel1"); 
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
    }

    public void ShowLevelMenu()
    {
        mainPanel.SetActive(false);
        LevelPanel.SetActive(true);
    }

    public void LoadTutorialLevel(int levelNumber)
    {
        // Load tutorial levels based on the selected number (1, 2, or 3)
        switch (levelNumber)
        {
            case 1:
                SceneManager.LoadScene("tutortiallevel1");
                break;
            case 2:
                SceneManager.LoadScene("tutoriallevel2");
                break;
            case 3:
                SceneManager.LoadScene("tutoriallevel3");
                break;
            default:
                Debug.LogError("Invalid tutorial level number");
                break;
        }
        ChangeButtonPattern();
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("level1"); // Loads "level1" scene directly
    }

}
