using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int currentLevel;
    private Dictionary<int, List<IEventTracker>> levelEventTrackerMap = new Dictionary<int, List<IEventTracker>>();
    private Dictionary<int, string> levelNameMap = new Dictionary<int, string>
    {
        { 1, "Tutorial 1" },
        { 2, "Tutorial 2" },
        { 3, "Tutorial 3" },
        { 4, "Level 1" },
        { 5, "Tutorial 4" },
        { 6, "Level 2" }
    };
    public Dictionary<int, string> levelSceneNameMap = new Dictionary<int, string>
    {
        { 1, "tutortiallevel1" },
        { 2, "tutoriallevel2" },
        { 3, "tutoriallevel3" },
        { 4, "level1" },
        { 5, "tutoriallevel4" },
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

    public void SetLevel(int newLevel)
    {
        currentLevel = newLevel;
    }

    public void RestartLevel()
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

        if (!levelEventTrackerMap.ContainsKey(currentLevel))
        {
            IEventTracker levelStartTracker = gameObject.AddComponent<LevelStartTracker>();
            levelStartTracker.Initialize(currentLevel);
            levelStartTracker.SendEvent();

            levelEventTrackerMap[currentLevel] = new List<IEventTracker>();
            IEventTracker levelCompleteTracker = gameObject.AddComponent<LevelCompleteTracker>();
            levelCompleteTracker.Initialize(currentLevel);
            levelEventTrackerMap[currentLevel].Add(levelCompleteTracker);
        }
        else
        {
            foreach (IEventTracker eventTracker in levelEventTrackerMap[currentLevel])
            {
                eventTracker.ResetTracker();
            }
        }

    }

    public void OnCompleteLevel()
    {
        if (currentLevel == -1)
        {
            return;
        }
        foreach (IEventTracker eventTracker in levelEventTrackerMap[currentLevel])
        {
            eventTracker.SendEvent();
        }
    }

    public void OnDestroyLevel()
    {
        if (currentLevel == -1)
        {
            return;
        }
        foreach (IEventTracker eventTracker in levelEventTrackerMap[currentLevel])
        {
            eventTracker.ResetTracker();
        }
    }
}
