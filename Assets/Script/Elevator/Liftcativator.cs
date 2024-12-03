using UnityEngine;

public class Liftcativator : MonoBehaviour, IResettable
{
    private bool _initialHasActivated;

    public LiftController LiftController; 
    private bool hasActivated = false; 

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
            if (LiftController != null)
            {
                LiftController.StartMovement(); // ????????????
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