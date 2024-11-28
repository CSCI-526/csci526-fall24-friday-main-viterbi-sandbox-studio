using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnMouseMove : MonoBehaviour
{
    private Material _originalMaterial; // Store the original material
    private Renderer _renderer;        // Reference to the object's renderer
    private Vector3 _lastMousePosition; // Track the last mouse position
    private float _lastMouseMoveTime;   // Time when the mouse was last detected as moving
    private bool _isMouseMoving;        // Flag to check if the mouse is moving

    [Header("Color Settings")]
    public Material changeToMaterial;  // Material to apply when the mouse moves

    [Header("Settings")]
    public float stopThreshold = 0.1f; // Time in seconds before considering the mouse "stopped"

    void Start()
    {
        // Get the Renderer component and store the original material
        _renderer = GetComponent<Renderer>();
        if (_renderer != null)
        {
            _originalMaterial = _renderer.material;
        }
        else
        {
            Debug.LogError("Renderer component not found on the object.");
        }

        // Initialize mouse tracking
        _lastMousePosition = Input.mousePosition;
        _lastMouseMoveTime = Time.time;
    }

    void Update()
    {
        CheckMouseMovement();

        if (_isMouseMoving)
        {
            ChangeToMaterial();
        }
        else
        {
            RevertToOriginal();
        }
    }

    void CheckMouseMovement()
    {
        // Detect if the mouse position has changed
        if (Input.mousePosition != _lastMousePosition)
        {
            _isMouseMoving = true;
            _lastMouseMoveTime = Time.time; // Update the last movement time
            _lastMousePosition = Input.mousePosition; // Update the last mouse position
        }
        else if (Time.time - _lastMouseMoveTime > stopThreshold)
        {
            // If no movement has occurred for `stopThreshold` seconds, consider the mouse stopped
            _isMouseMoving = false;
        }
    }

    void ChangeToMaterial()
    {
        if (_renderer != null && changeToMaterial != null && _renderer.material != changeToMaterial)
        {
            _renderer.material = changeToMaterial;
        }
    }

    void RevertToOriginal()
    {
        if (_renderer != null && _originalMaterial != null && _renderer.material != _originalMaterial)
        {
            _renderer.material = _originalMaterial;
        }
    }
}

