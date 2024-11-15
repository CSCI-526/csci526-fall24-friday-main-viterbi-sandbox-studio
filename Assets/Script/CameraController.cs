using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform cameraPivot1;
    [SerializeField] private Transform cameraPivot2; // optional
    public LayerMask wallLayerMask; // Layer mask to detect walls

    private Transform currentPivot;
    private Vector3 initialOffset = new Vector3(0, 1, -7);
    private float sensitivity = 800f;
    private int minVerticalAngle = -20;
    private int maxVerticalAngle = 60;
    private Vector3 previousPosition;

    private float currentZoomDistance;
    private float maxZoomDistance;
    private float zoomSpeed = 10f;

    private float horizontalAngle = 90f;
    private float verticalAngle = 0f;


    // Start is called before the first frame update
    void Start()
    {
        maxZoomDistance = initialOffset.magnitude;
        currentZoomDistance = maxZoomDistance;
        previousPosition = Vector3.zero;

        currentPivot = cameraPivot1;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
        AdjustCameraZoom();
    }

    void RotateCamera()
    {
        if (PersistentMenu.instance.IsInTransitOrMenuOpened())
        {
            return;
        }

        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Update angles
        horizontalAngle += mouseX * sensitivity * Time.deltaTime;
        verticalAngle -= mouseY * sensitivity * Time.deltaTime;
        verticalAngle = Mathf.Clamp(verticalAngle, minVerticalAngle, maxVerticalAngle);

        // Calculate rotation
        Quaternion rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0);

        // Update camera position
        Vector3 direction = rotation * Vector3.forward;
        cam.transform.position = currentPivot.position - direction * currentZoomDistance;

        // Make the camera look at the pivot
        cam.transform.LookAt(currentPivot.position);
    }

    void ClampVerticalRotation()
    {
        Vector3 eulerAngles = cam.transform.eulerAngles;
        eulerAngles.x = ClampAngle(eulerAngles.x, minVerticalAngle, maxVerticalAngle);
        cam.transform.eulerAngles = eulerAngles;
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle > 180) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    void AdjustCameraZoom()
    {
        RaycastHit hit;
        Vector3 directionToCamera = (cam.transform.position - currentPivot.position).normalized;

        // Raycast from the camera pivot to the camera's position to detect obstacles
        if (Physics.Raycast(currentPivot.position, directionToCamera, out hit, maxZoomDistance, wallLayerMask))
        {
            // If an obstacle is detected, adjust the zoom distance to avoid it
            currentZoomDistance = Mathf.Clamp(Mathf.Lerp(currentZoomDistance, hit.distance - 0.2f, Time.deltaTime * zoomSpeed), 1.5f, maxZoomDistance);
        }
        else
        {
            // No obstacles, return to the initial zoom distance
            currentZoomDistance = Mathf.Lerp(currentZoomDistance, maxZoomDistance, Time.deltaTime * zoomSpeed);
        }

        // Position the camera at the new zoom distance
        cam.transform.position = currentPivot.position + directionToCamera * currentZoomDistance;
    }

    public void SwitchCameraPivot()
    {
        if (cameraPivot2 != null)
        {
            currentPivot = (currentPivot == cameraPivot1) ? cameraPivot2 : cameraPivot1;
        }
    }
}
