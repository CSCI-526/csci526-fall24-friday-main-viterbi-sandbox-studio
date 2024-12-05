using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the camera transform

    public float rotationSpeed = 10f;
    public float speed = 7f;
    public float jumpForce = 11f;
    protected virtual float distanceToGround { get; }

    private Rigidbody rb;           

    public GameObject arrowPrefab;   
    private GameObject arrowInstance;

    [SerializeField] private string floorLayerName = "Floor";
    [SerializeField] private string platformLayerName = "Platform";
    [SerializeField] private string boxLayerName = "Box";
    [SerializeField] private string jumpableLayerName = "Jumpable";
    protected int combinedLayerMask;

    private void Awake()
    {
        // Get the layer mask for the specified floor layer
        combinedLayerMask = LayerMask.GetMask(floorLayerName)
            | LayerMask.GetMask(platformLayerName)
            | LayerMask.GetMask(boxLayerName)
            | LayerMask.GetMask(jumpableLayerName);
    }

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

    protected virtual bool IsGrounded()
    {
        return false; // Default behavior, should be overridden by child classes
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