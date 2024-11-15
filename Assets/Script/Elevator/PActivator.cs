using UnityEngine;

public class PActivator : MonoBehaviour, IResettable
{
    private bool _initialHasActivated;

    public EController elevatorController; // ????ElevatorController
    private bool hasActivated = false; // ????????????????????????

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasActivated)
        {
            if (elevatorController != null)
            {
                elevatorController.StartMovement(); // ????????????
                hasActivated = true; // ??????????????????????????
                Debug.Log("Platform activated by Player.");
            }
            else
            {
                Debug.LogWarning("ElevatorController reference is missing.");
            }
        }
    }
}
