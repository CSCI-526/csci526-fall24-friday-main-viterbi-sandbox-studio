using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
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
        l1Button.onClick.AddListener(() => LoadLevel(1));
        l2Button.onClick.AddListener(() => LoadLevel(2));
        l3Button.onClick.AddListener(() => LoadLevel(3));
        l4Button.onClick.AddListener(() => LoadLevel(4));
        backButton.onClick.AddListener(ShowMainMenu);

        gameManager = FindObjectOfType<GameManager>();
    }

    private void StartGame()
    {
        PersistentMenu.instance.inTransit = false;
        int levelNumber = 1;
        bool result = gameManager.StartLevel(levelNumber);
        if (!result)
        {
            Debug.LogError("Invalid level number");
            return;
        }
        InactiveMenu();
        ChangeButtonPattern();
    }

    private void ChangeButtonPattern()
    {
        startGameButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    private void ShowMainMenu()
    {
        PersistentMenu.instance.ShowMainMenu();
    }

    private void ContinueGame()
    {
        if (PersistentMenu.instance.inTransit)
        {
            bool hasTransitioned = gameManager.AdvanceToNextLevel();
            PersistentMenu.instance.inTransit = false;
            if (!hasTransitioned)
            {
                // TODO: Show Win Game
                return;
            }
        }
        InactiveMenu();
        PersistentMenu.instance.HideWinContext();
    }

    private void ShowLevelMenu()
    {
        mainPanel.SetActive(false);
        LevelPanel.SetActive(true);
    }

    private void InactiveMenu()
    {
        PersistentMenu.instance.HideMainMenu();
    }

    public void LoadLevel(int levelNumber)
    {
        bool result = gameManager.StartLevel(levelNumber);
        if (!result)
        {
            Debug.LogError("Invalid tutorial level number");
            return;
        }
        InactiveMenu();
        ChangeButtonPattern();
        PersistentMenu.instance.HideWinContext();
        PersistentMenu.instance.inTransit = false;
    }

}
