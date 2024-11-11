using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void RestartLevel()
    {
        FindObjectOfType<PlayerRespawn>().Respawn();
        FindObjectOfType<LevelResetManager>().ResetCurrentLevelObjects();
    }

    public bool StartLevel(int level)
    {
        if (!levelManager.levelSceneNameMap.ContainsKey(level))
        {
            return false;
        }
        levelManager.SetLevel(level);
        string levelSceneName = levelManager.GetCurrentLevelSceneName();
        SceneManager.LoadScene(levelSceneName);
        return true;
    }

    public bool AdvanceToNextLevel()
    {
        levelManager.LevelUp();
        if (!levelManager.levelSceneNameMap.ContainsKey(levelManager.GetLevel()))
        {
            return false;
        }
        string nextSceneName = levelManager.GetCurrentLevelSceneName();
        SceneManager.LoadScene(nextSceneName);
        return true;

    }
}
