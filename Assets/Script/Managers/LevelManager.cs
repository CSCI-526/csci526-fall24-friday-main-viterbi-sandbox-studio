using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int currentLevel;
    private Dictionary<int, string> levelNameMap = new Dictionary<int, string>
    {
        { 1, "Tutorial 1" },
        { 2, "Tutorial 2" },
        { 3, "Tutorial 3" },
        { 4, "Level 1" },
        //{ 5, "Tutorial 4" },
        //{ 6, "Level 2" }
    };
    public Dictionary<int, string> levelSceneNameMap = new Dictionary<int, string>
    {
        { 1, "tutortiallevel1" },
        { 2, "tutoriallevel2" },
        { 3, "tutoriallevel3" },
        { 4, "level1" },
        //{ 5, "tutoriallevel4" },
        //{ 6, "level2" }
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

    public void LevelUp()
    {
        if (!levelNameMap.ContainsKey(currentLevel + 1))
        {
            return;
        }
        currentLevel += 1;
        Debug.Log("currentLevel is " + currentLevel);
    }

    public void OnStartLevel()
    {
        LevelPassingTimeTracker levelPassingTimeTracker = gameObject.AddComponent<LevelPassingTimeTracker>();
        levelPassingTimeTracker.Initialize(currentLevel);
        CharacterSwitchTracker characterSwitchTracker = gameObject.AddComponent<CharacterSwitchTracker>();
        characterSwitchTracker.Initialize(currentLevel);

    }

    public void OnCompleteLevel()
    {
        EventManager.Instance.SendEvent($"LevelPassingTime_{currentLevel}");
        EventManager.Instance.SendEvent($"CharacterSwitchCount_{currentLevel}");
    }

    public void OnDestroyLevel()
    {
        if (currentLevel == -1)
        {
            return;
        }
        EventManager.Instance.UnregisterTracker($"LevelPassingTime_{currentLevel}");
        EventManager.Instance.UnregisterTracker($"CharacterSwitchCount_{currentLevel}");
    }
}
