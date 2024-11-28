using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnKeyPress : MonoBehaviour
{
    private Material _originalMaterial; // Store the original material
    private Renderer _renderer;        // Reference to the object's renderer

    [Header("Key Settings")]
    public KeyCode key = KeyCode.A;    // Key to trigger the color change

    [Header("Color Settings")]
    public Material changeToMaterial;  // Material to apply when the key is pressed

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
    }

    void Update()
    {
        // Check if the key is pressed
        if (Input.GetKeyDown(key))
        {
            ChangeToMaterial();
        }
        // Check if the key is released
        else if (Input.GetKeyUp(key))
        {
            RevertToOriginal();
        }
    }

    void ChangeToMaterial()
    {
        if (_renderer != null && changeToMaterial != null)
        {
            _renderer.material = changeToMaterial;
        }
    }

    void RevertToOriginal()
    {
        if (_renderer != null && _originalMaterial != null)
        {
            _renderer.material = _originalMaterial;
        }
    }
}

