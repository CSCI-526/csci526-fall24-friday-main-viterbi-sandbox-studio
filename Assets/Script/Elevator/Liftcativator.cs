using UnityEngine;

public class Liftcativator : MonoBehaviour, IResettable
{
    private bool _initialHasActivated;

    public LiftController LiftController;
    public AudioClip activationSound; // Sound effect for lift activation

    private AudioSource audioSource;  // Audio source to play the sound
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

        // Add or configure an AudioSource on this GameObject
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = activationSound; // Assign the activation sound effect

        // Debug log to confirm setup
        if (activationSound == null)
        {
            Debug.LogWarning("Activation sound effect is not assigned in the Inspector!");
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
            if (LiftController != null)
            {
                // Start the lift movement
                LiftController.StartMovement();

                // Play the activation sound effect
                if (audioSource != null && activationSound != null)
                {
                    audioSource.Play();
                    Debug.Log("Activation sound effect played.");
                }
                else
                {
                    Debug.LogWarning("Activation sound effect or AudioSource is not set!");
                }

                hasActivated = true;
                Debug.Log("Platform activated by Player.");
            }
            else
            {
                Debug.LogWarning("LiftController reference is missing.");
            }
        }
    }
}
