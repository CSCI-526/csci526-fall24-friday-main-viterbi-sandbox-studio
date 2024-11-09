using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResetManager : MonoBehaviour
{
    private LevelManager levelManager;
    private Dictionary<int, List<IResettable>> levelObjects = new Dictionary<int, List<IResettable>>();

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    public void RegisterObject(int level, IResettable levelObject)
    {
        if (!levelObjects.ContainsKey(level))
        {
            levelObjects[level] = new List<IResettable>();
        }
        levelObjects[level].Add(levelObject);
    }

    public void ResetCurrentLevelObjects()
    {
        int currentLevel = levelManager.getLevel();
        if (levelObjects.ContainsKey(currentLevel))
        {
            foreach (var obj in levelObjects[currentLevel])
            {
                Debug.Log("Initial obj");
                obj.ResetState();
            }
        }
    }
}
