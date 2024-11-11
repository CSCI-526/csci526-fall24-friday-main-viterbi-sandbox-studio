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

    public void SaveInitialState()
    {
        _initialIsMoving = isMoving;
        _initialMovingUp = movingUp;
    }

    public void ResetState()
    {
        isMoving = _initialIsMoving;
        movingUp = _initialMovingUp;
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