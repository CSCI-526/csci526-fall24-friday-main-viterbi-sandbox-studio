using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int currentLevel;
    private Dictionary<int, string> levelMap = new Dictionary<int, string>
    {
        { 1, "Tutorial 1" },
        { 2, "Tutorial 2" },
        { 3, "Tutorial 3" },
        { 4, "Level 1" },
        { 5, "Tutorial 4" },
        { 6, "Level 2" }
    };

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 1;
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
        return levelMap[currentLevel];
    }

    public void LevelUp()
    {
        currentLevel += 1;
        Debug.Log("currentLevel is " + currentLevel);
    }

    public void ResetLevel()
    {
        currentLevel = 1;
    }
}
