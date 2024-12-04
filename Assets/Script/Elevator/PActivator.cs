using UnityEngine;

public class PActivator : MonoBehaviour, IResettable
{
    private bool _initialHasActivated;

    public EController elevatorController; 
    private bool hasActivated = false;
    // Delegate for custom reset logic
    private System.Action _customResetAction;

    public void SaveInitialState()
    {
        _initialHasActivated = hasActivated;
    }

    public void ResetState()
    {
        hasActivated = _initialHasActivated;
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

    public void UpdateState(System.Action customResetAction = null)
    {
        _initialHasActivated = hasActivated;

        if (customResetAction != null)
        {
            _customResetAction = customResetAction;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasActivated)
        {
            if (elevatorController != null)
            {
                elevatorController.StartMovement(); 
                hasActivated = true; 
                Debug.Log("Platform activated by Player.");
            }
            else
            {
                Debug.LogWarning("ElevatorController reference is missing.");
            }
        }
    }
}
