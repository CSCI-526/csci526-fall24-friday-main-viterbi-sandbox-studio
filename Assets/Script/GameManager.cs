using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void RestartLevel()
    {
        FindObjectOfType<PlayerRespawn>().Respawn();
        FindObjectOfType<LevelResetManager>().ResetCurrentLevelObjects();
    }
}
