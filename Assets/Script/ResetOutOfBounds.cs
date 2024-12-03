using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOutOfBounds : MonoBehaviour
{
    private LevelResetManager levelResetManager;
    private SceneTransitionManager sceneTransitionManager;
    // Start is called before the first frame update
    void Start()
    {
        levelResetManager = FindObjectOfType<LevelResetManager>();
        sceneTransitionManager = FindObjectOfType<SceneTransitionManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ShouldTriggerReset(other))
        {
            StartCoroutine(ResetGame());
        }
    }

    private bool ShouldTriggerReset(Collider other)
    {
        return other.CompareTag("Player") ||
            other.CompareTag("BoxLevelF");
    }

    private IEnumerator ResetGame()
    {
        PersistentMenu.instance.inTransit = true;
        yield return StartCoroutine(sceneTransitionManager.FadeOut());
        levelResetManager.ResetCurrentLevelObjects();
        yield return new WaitForSeconds(1f);
        PersistentMenu.instance.inTransit = false;
        yield return StartCoroutine(sceneTransitionManager.FadeIn());
    }
}
