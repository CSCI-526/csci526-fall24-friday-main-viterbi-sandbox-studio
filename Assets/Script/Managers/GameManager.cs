using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private LevelManager levelManager;
    private LevelResetManager levelResetManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        levelResetManager = FindObjectOfType<LevelResetManager>();
    }

    public void ReloadScene()
    {
        levelManager.OnDestroyLevel();
        levelResetManager.UnregisterCurrentLevelObjects();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        levelManager.OnStartLevel();
    }

    public bool StartLevel(int level)
    {
        if (!levelManager.levelSceneNameMap.ContainsKey(level))
        {
            return false;
        }
        levelManager.OnDestroyLevel();
        levelManager.SetLevel(level);
        string levelSceneName = levelManager.GetCurrentLevelSceneName();
        SceneManager.LoadScene(levelSceneName);
        levelManager.OnStartLevel();
        return true;
    }

    public bool AdvanceToNextLevel()
    {
        levelManager.OnCompleteLevel();
        levelResetManager.UnregisterCurrentLevelObjects();
        levelManager.OnDestroyLevel();
        levelManager.LevelUp();
        if (!levelManager.levelSceneNameMap.ContainsKey(levelManager.GetLevel()))
        {
            return false;
        }
        string nextSceneName = levelManager.GetCurrentLevelSceneName();
        SceneManager.LoadScene(nextSceneName);
        levelManager.OnStartLevel();
        return true;

    }
}
