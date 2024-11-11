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
    public void RegisterObject(IResettable levelObject)
    {
        int currentLevel = levelManager.GetLevel();
        if (!levelObjects.ContainsKey(currentLevel))
        {
            levelObjects[currentLevel] = new List<IResettable>();
        }
        levelObjects[currentLevel].Add(levelObject);
    }

    public void ResetCurrentLevelObjects()
    {
        int currentLevel = levelManager.GetLevel();
        if (levelObjects.ContainsKey(currentLevel))
        {
            foreach (var obj in levelObjects[currentLevel])
            {
                Debug.Log("Initial obj");
                obj.ResetState();
            }
        }
    }

    public void UnregisterLevelObjects()
    {
        int currentLevel = levelManager.GetLevel();
        // Check if the level exists in the dictionary
        if (levelObjects.ContainsKey(currentLevel))
        {

            levelObjects[currentLevel].Clear();
            //levelObjects.Remove(currentLevel);
            Debug.Log($"Unregistered all objects from level {currentLevel}");
        }
    }
}
