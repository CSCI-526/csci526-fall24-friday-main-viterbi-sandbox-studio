using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour, IResettable
{

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private bool _initialIsMoving;
    private bool _initialMovingUp;

    public float speed = 3f;         
    public float lowerHeight = 56.8235f;   
    public float upperHeight = 63f;  

    private bool isMoving = false;   
    private bool movingUp = true;

    // Delegate for custom reset logic
    private System.Action _customResetAction;

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

    public void UpdateState(System.Action customResetAction = null)
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;

        if (customResetAction != null)
        {
            _customResetAction = customResetAction;
        }
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
                if (transform.position.x < upperHeight)
                {
                    transform.position += Vector3.right * speed * Time.deltaTime;
                }
                else
                {
                    movingUp = false;
                }
            }
            else
            {
                if (transform.position.x > lowerHeight)
                {
                    transform.position += Vector3.left* speed * Time.deltaTime; 
                }
                else
                {
                    movingUp = true;
                }
            }
        }
    }
}

