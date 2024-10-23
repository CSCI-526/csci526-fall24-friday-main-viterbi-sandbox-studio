using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getLevel()
    {
        return currentLevel;
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
