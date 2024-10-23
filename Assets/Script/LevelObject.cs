using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour, IResettable
{
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    [SerializeField]
    public int level; // The level this object belongs to

    public void SaveInitialState()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        // Save additional properties here (e.g., health, color)
    }

    public void ResetState()
    {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
        // Reset other properties here
    }

    private void Start()
    {
        SaveInitialState();
        FindObjectOfType<LevelResetManager>().RegisterObject(level, this);
    }
}
