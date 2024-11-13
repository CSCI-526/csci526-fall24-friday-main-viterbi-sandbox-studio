using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public LiftController elevatorController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            elevatorController.ToggleMovement(); 
        }
    }
}