using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private LevelManager levelManager;
    private LevelResetManager levelResetManager;
    private SceneTransitionManager sceneTransitionManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        levelResetManager = FindObjectOfType<LevelResetManager>();
        sceneTransitionManager = FindObjectOfType<SceneTransitionManager>();
    }

    public void ReloadScene()
    {
        levelManager.OnDestroyLevel();
        levelResetManager.UnregisterCurrentLevelObjects();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        levelManager.OnRestartLevel();
    }

    public IEnumerator StartLevel(int level)
    {
        if (!levelManager.levelSceneNameMap.ContainsKey(level))
        {
            yield break;
        }
        levelManager.OnDestroyLevel();
        levelManager.SetLevel(level);
        yield return StartCoroutine(HandleSceneTransition(false));
    }

    public void TriggerCompleteLevel()
    {
        StartCoroutine(CompleteLevel());
    }

    public IEnumerator CompleteLevel()
    {
        PersistentMenu.instance.inTransit = true;
        levelManager.OnCompleteLevel();
        if (levelManager.isLastLevel())
        {
            
            PersistentMenu.instance.ShowWinEnd();
            // TODO: Add win context
            yield return GoBackToMenu();
        }
        else
        {
            // TODO: Add win context (showWinContext won't work)
            //PersistentMenu.instance.showWinContext();
            //PersistentMenu.instance.ShowMainMenu();
            yield return AdvanceToNextLevel();
        }   
    }

    public IEnumerator AdvanceToNextLevel()
    {
        levelResetManager.UnregisterCurrentLevelObjects();
        levelManager.OnDestroyLevel();

        if (!levelManager.LevelUp())
        {
            Debug.Log("LevelUp failed, breaking coroutine.");
            yield break;
        }
        Debug.Log("LevelUp succeeded, proceeding to HandleSceneTransition.");
        yield return StartCoroutine(HandleSceneTransition(true));

    }

    public IEnumerator GoBackToMenu()
    {
        Debug.Log("Go back to main menu");
        levelManager.ResetLevel();

        MainMenuController mainMenuController = FindObjectOfType<MainMenuController>();
        mainMenuController.ResetButtonPattern();

        yield return StartCoroutine(HandleSceneTransition(true));
    }

    private IEnumerator HandleSceneTransition(bool useFadeOutDelay)
    {
        string nextSceneName = levelManager.GetCurrentLevelSceneName();

        if (useFadeOutDelay)
        {
            yield return StartCoroutine(sceneTransitionManager.FadeOutDelay());
            
        }
        yield return StartCoroutine(sceneTransitionManager.FadeOut());
        //PersistentMenu.instance.HideWinEnd();
        SceneManager.LoadScene(nextSceneName);
        PersistentMenu.instance.HideMainMenu();
        PersistentMenu.instance.inTransit = false;
        levelManager.OnStartLevel();
        yield return null; // Wait a frame to ensure the new scene is loaded

        yield return StartCoroutine(sceneTransitionManager.FadeIn());
        
    }
}
