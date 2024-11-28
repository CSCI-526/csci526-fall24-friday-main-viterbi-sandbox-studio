using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnMouseMove : MonoBehaviour
{
    private Material _originalMaterial; // Store the original material
    private Renderer _renderer;        // Reference to the object's renderer
    private float _lastMouseMoveTime;   // Time when the mouse was last detected as moving
    private bool _isMouseMoving;        // Flag to check if the mouse is moving

    [Header("Color Settings")]
    public Material changeToMaterial;  // Material to apply when the mouse moves

    [Header("Settings")]
    public float stopThreshold = 0.1f; // Time in seconds before considering the mouse "stopped"
    public float sensitivityThreshold = 0.01f; // Minimum movement to count as "moving"

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
        // Use Input.GetAxis to detect relative mouse movement
        float mouseDeltaX = Input.GetAxis("Mouse X");
        float mouseDeltaY = Input.GetAxis("Mouse Y");

        // Check if mouse movement exceeds sensitivity threshold
        if (Mathf.Abs(mouseDeltaX) > sensitivityThreshold || Mathf.Abs(mouseDeltaY) > sensitivityThreshold)
        {
            _isMouseMoving = true;
            _lastMouseMoveTime = Time.time; // Update the last movement time
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

