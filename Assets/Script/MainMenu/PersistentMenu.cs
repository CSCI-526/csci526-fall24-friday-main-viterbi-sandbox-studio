using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentMenu : MonoBehaviour
{
    private static PersistentMenu instance;
    public GameObject mainPanel; 
    public GameObject LevelPanel;

    private void Awake()
    {
        // Ensure only one instance of the canvas exists across scenes
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Make this canvas persistent

        // Subscribe to scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Hide the main menu canvas if not in the main menu scene
        LevelPanel.SetActive(false);
        if (scene.name == "MainMenuScene")
        {
            mainPanel.SetActive(true);
        }
        else
        {
            mainPanel.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the scene loaded event when the object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
