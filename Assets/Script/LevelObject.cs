using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour, IResettable
{
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    public string checkpointType;

    // Delegate for custom reset logic
    private System.Action _customResetAction;

    public void SaveInitialState()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    public void ResetState()
    {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;

        // Invoke custom reset logic if defined
        _customResetAction?.Invoke();
    }

    public void UpdateState(System.Action customResetAction = null)
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;

        if (customResetAction != null)
        {
            _customResetAction = customResetAction;
        }
    }

    private void Start()
    {
        SaveInitialState();
        LevelResetManager levelResetManager = FindObjectOfType<LevelResetManager>();
        if (levelResetManager != null)
        {
            levelResetManager.RegisterObject(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (checkpointType != null && other.CompareTag(checkpointType))
        {
            UpdateState();
        }
    }
}
