using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the camera transform

    public float rotationSpeed = 10f;
    public float speed = 7f;
    public float jumpForce = 11f;
    public float distanceToGround = 2.0f;
    public bool isRectangular = true;
    private Rigidbody rb;           

    public GameObject arrowPrefab;   
    private GameObject arrowInstance; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        arrowInstance = Instantiate(arrowPrefab, transform.position + Vector3.up * 3, Quaternion.identity);
        arrowInstance.transform.SetParent(transform);  
        arrowInstance.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && !PersistentMenu.instance.IsInTransitOrMenuOpened())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (!PersistentMenu.instance.IsInTransitOrMenuOpened())
        {        
            RotatePlayerToCameraDirection();

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(verticalInput, 0, -horizontalInput) * speed * Time.deltaTime;

            transform.Translate(movement);
        }
    }

    private bool IsGrounded()
    {
        if (isRectangular)
        {
            return IsBoxGrounded();
        }
        return IsSphereGrounded();
    }

    private bool IsBoxGrounded()
    {
        Vector3 boxSize = new Vector3(transform.localScale.x / 2, 0.1f, transform.localScale.z / 2);
        return Physics.BoxCast(transform.position, boxSize, Vector3.down, Quaternion.identity, distanceToGround + 0.1f);
    }

    private bool IsSphereGrounded()
    {
        float sphereRadius = transform.localScale.x / 2;
        return Physics.SphereCast(transform.position, sphereRadius, Vector3.down, out RaycastHit hit, distanceToGround + 0.1f);
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