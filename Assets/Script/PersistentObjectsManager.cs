using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectsManager : MonoBehaviour
{
    private static PersistentObjectsManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //private void Start()
    //{
    //    Debug.Log("PersistentObjects is still active in the new scene.");
    //}
}
