using UnityEngine;

public class EController : MonoBehaviour, IResettable
{
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private bool _initialIsMoving;
    private bool _initialMovingUp;

    public float speed = 3f;         
    public float lowerHeight = -2f;   
    public float upperHeight = 13f;  

    private bool isMoving = false;   
    private bool movingUp = true;

    public void SaveInitialState()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _initialIsMoving = isMoving;
        _initialMovingUp = movingUp;
    }

    public void ResetState()
    {
        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
        isMoving = _initialIsMoving;
        movingUp = _initialMovingUp;
    }

    public void StartMovement()
    {
        isMoving = true;
        Debug.Log("Elevator movement started.");
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
