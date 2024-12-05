using System;
using System.Collections;
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
    public Button controlButton;

    public GameObject mainPanel;
    public GameObject LevelPanel;
    public GameObject ControlPanel;

    public Button l1Button;
    public Button l2Button;
    public Button l3Button;
    public Button l4Button;
    public Button l5Button;
    public Button l6Button;
    public Button l7Button;
    public Button backButton;
    public Button backButton2;

    private GameManager gameManager;

    private void Start()
    {
        // Assign the StartGame function to the button's onClick event
        startGameButton.onClick.AddListener(() => StartCoroutine(LoadLevel(1)));
        menuButton.onClick.AddListener(ShowMainMenu);
        continueButton.onClick.AddListener(ContinueGame);
        levelButton.onClick.AddListener(ShowLevelMenu);
        controlButton.onClick.AddListener(ShowControlPanel);
        l1Button.onClick.AddListener(() => StartCoroutine(LoadLevel(1)));
        l2Button.onClick.AddListener(() => StartCoroutine(LoadLevel(2)));
        l3Button.onClick.AddListener(() => StartCoroutine(LoadLevel(3)));
        l4Button.onClick.AddListener(() => StartCoroutine(LoadLevel(4)));
        l5Button.onClick.AddListener(() => StartCoroutine(LoadLevel(5)));
        l6Button.onClick.AddListener(() => StartCoroutine(LoadLevel(6)));
        l7Button.onClick.AddListener(() => StartCoroutine(LoadLevel(7)));
        backButton.onClick.AddListener(ShowMainMenu);
        backButton2.onClick.AddListener(ShowMainMenu);

        gameManager = FindObjectOfType<GameManager>();
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
        InactiveMenu();
    }

    private void ShowControlPanel()
    {
        mainPanel.SetActive(false);
        ControlPanel.SetActive(true);
    }

    public void ResetButtonPattern()
    {
        startGameButton.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
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

    public IEnumerator LoadLevel(int levelNumber)
    {
        yield return gameManager.StartLevel(levelNumber);

        ChangeButtonPattern();
        PersistentMenu.instance.inTransit = false;
    }

}
