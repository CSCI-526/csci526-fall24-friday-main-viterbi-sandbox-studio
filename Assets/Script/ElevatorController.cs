using UnityEngine;

public class ElevatorController : MonoBehaviour, IResettable
{
    public float speed = 2f;
    public float lowerHeight = 1f;
    public float upperHeight = 10f;

    private bool movingUp = true;          
    private bool isMoving = false;

    private bool _initialIsMoving;
    private bool _initialMovingUp;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private System.Action _customResetAction;

    public void SaveInitialState()
    {
        _initialIsMoving = isMoving;
        _initialMovingUp = movingUp;

        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    public void ResetState()
    {
        isMoving = _initialIsMoving;
        movingUp = _initialMovingUp;
        _customResetAction?.Invoke();
    }

    public void UpdateState(System.Action customResetAction = null)
    {
        _initialIsMoving = isMoving;
        _initialMovingUp = movingUp;

        _initialPosition = transform.position;
        _initialRotation = transform.rotation;

        if (customResetAction != null)
        {
            _customResetAction = customResetAction;
        }
    }

    public void ToggleMovement()
    {
        isMoving = !isMoving; 
    }

    void Start()
    {
        SaveInitialState();
        FindObjectOfType<LevelResetManager>().RegisterObject(this);
    }

    private void Update()
    {
        if (isMoving) 
        {
            if (movingUp)
            {
                if (transform.position.y < upperHeight)
                {
                    transform.position += Vector3.up * speed * Time.deltaTime;
                }
                else
                {
                    movingUp = false; 
                }
            }
            else
            {
                if (transform.position.y > lowerHeight)
                {
                    transform.position += Vector3.down * speed * Time.deltaTime;
                }
                else
                {
                    movingUp = true; 
                }
            }
        }
    }
}