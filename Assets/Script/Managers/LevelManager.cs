using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int currentLevel;
    private int tutorialCount = 4;
    private int maxLevel = 6;
    private Dictionary<int, LevelStartTracker> levelStartTrackerMap = new Dictionary<int, LevelStartTracker>();
    private Dictionary<int, LevelCompleteTracker> levelCompleteTrackerMap = new Dictionary<int, LevelCompleteTracker>();
    private Dictionary<int, string> levelNameMap = new Dictionary<int, string>
    {
        { 1, "Tutorial 1" },
        { 2, "Tutorial 2" },
        { 3, "Tutorial 3" },
        { 5, "Level 1" },
        { 4, "Tutorial 4" },
        { 6, "Level 2" }
    };
    public Dictionary<int, string> levelSceneNameMap = new Dictionary<int, string>
    {
        { 1, "tutortiallevel1" },
        { 2, "tutoriallevel2" },
        { 3, "tutoriallevel3" },
        { 5, "level1" },
        { 4, "tutoriallevel4" },
        { 6, "level2" }
    };

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetLevel()
    {
        return currentLevel;
    }

    public string GetCurrentLevelName()
    {
        return levelNameMap[currentLevel];
    }

    public string GetCurrentLevelSceneName()
    {
        return levelSceneNameMap[currentLevel];
    }

    public LevelCompleteTracker GetCurrentLevelCompleteTracker()
    {
        return levelCompleteTrackerMap[currentLevel];
    }

    public void SetLevel(int newLevel)
    {
        currentLevel = newLevel;
    }

    public void ResetPlayerAndObjects()
    {
        FindObjectOfType<PlayerRespawn>().Respawn();
        FindObjectOfType<LevelResetManager>().ResetCurrentLevelObjects();
    }

    public bool LevelUp()
    {
        if (!levelNameMap.ContainsKey(currentLevel + 1))
        {
            return false;
        }
        currentLevel += 1;
        Debug.Log("currentLevel is " + currentLevel);
        return true;
    }

    public void OnStartLevel()
    {
        if (currentLevel == -1)
        {
            return;
        }

        if (!levelCompleteTrackerMap.ContainsKey(currentLevel))
        {
            LevelStartTracker levelStartTracker = gameObject.AddComponent<LevelStartTracker>();
            levelStartTrackerMap.Add(currentLevel, levelStartTracker);
            levelStartTracker.Initialize(currentLevel);
            levelStartTracker.SendEvent();

            LevelCompleteTracker levelCompleteTracker = gameObject.AddComponent<LevelCompleteTracker>();
            levelCompleteTrackerMap.Add(currentLevel, levelCompleteTracker);
            levelCompleteTracker.Initialize(currentLevel);
        }
        else
        {
            OnRestartLevel();
        }

    }

    public void OnRestartLevel()
    {
        if (currentLevel == -1)
        {
            return;
        }
        levelStartTrackerMap[currentLevel].SendEvent();
        levelCompleteTrackerMap[currentLevel].ResetTracker();
    }

    public void OnCompleteLevel()
    {
        if (currentLevel == -1)
        {
            return;
        }
        levelCompleteTrackerMap[currentLevel].SendEvent();
    }

    public void OnDestroyLevel()
    {
        if (currentLevel == -1)
        {
            return;
        }
        levelCompleteTrackerMap[currentLevel].InitializeParams();
    }

    public bool isInTutorial()
    {
        return currentLevel <= tutorialCount;
    }

    public bool isLastLevel()
    {
        return currentLevel == maxLevel;
    }
}
