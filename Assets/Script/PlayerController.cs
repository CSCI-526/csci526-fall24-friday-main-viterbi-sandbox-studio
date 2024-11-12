using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the camera transform

    public float rotationSpeed = 10f;
    public float speed = 7f;
    public float jumpForce = 11f;
    public float distanceToGround = 1.8f;
    private float jumpCooldown = 0.6f;  // 0.6 second cooldown
    private float lastJumpTime;
    private Rigidbody rb;           

    public GameObject arrowPrefab;   
    private GameObject arrowInstance; 

    private CharacterSwitcher characterSwitcher;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterSwitcher = FindObjectOfType<CharacterSwitcher>();

        
        arrowInstance = Instantiate(arrowPrefab, transform.position + Vector3.up * 3, Quaternion.identity);
        arrowInstance.transform.SetParent(transform);  
        arrowInstance.SetActive(true);

        lastJumpTime = -jumpCooldown;
    }

    void FixedUpdate()
    {
        RotatePlayerToCameraDirection();

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(verticalInput, 0, -horizontalInput) * speed * Time.deltaTime;

        transform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && Time.time >= lastJumpTime + jumpCooldown)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            lastJumpTime = Time.time;
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f);
    }

    public void ToggleArrow(bool showArrow)
    {
        if (arrowInstance != null)
        {
            arrowInstance.SetActive(showArrow);
        }
    }

    void RotatePlayerToCameraDirection()
    {
        // Get the direction the camera is facing
        Vector3 cameraForward = Quaternion.Euler(0, -90, 0) * cameraTransform.forward;
        cameraForward.y = 0; // Ignore vertical component to keep player upright

        // Only rotate if there's movement input to avoid unwanted rotations
        if (cameraForward.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed); // Smooth rotation
        }
    }
}