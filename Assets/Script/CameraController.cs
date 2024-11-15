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
    public float sensitivity = 270f;
    private int minVerticalAngle = -20;
    private int maxVerticalAngle = 60;
    private Vector3 previousPosition;

    private float currentZoomDistance;
    private float maxZoomDistance;
    private float zoomSpeed = 10f;


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
            previousPosition = Vector3.zero;
            return;
        }
        cam.transform.position = currentPivot.position;
        // Capture the mouse position on the first frame
        if (previousPosition == Vector3.zero)
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);


        cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * sensitivity);
        cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * sensitivity, Space.World);

        ClampVerticalRotation();

        previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        cam.transform.Translate(initialOffset);
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
