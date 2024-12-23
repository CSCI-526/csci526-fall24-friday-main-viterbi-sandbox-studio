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
    private Vector3 initialOffset = new Vector3(0, 0, 10);
    private float sensitivity = 200f;
    private int minVerticalAngle = -20;
    private int maxVerticalAngle = 60;

    private float currentZoomDistance;
    private float maxZoomDistance;
    private float zoomSpeed = 10f;

    private float horizontalAngle = 90f;
    private float verticalAngle = 25f;

    private int reverseCamera;
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {

        maxZoomDistance = initialOffset.magnitude;
        currentZoomDistance = maxZoomDistance;

        currentPivot = cameraPivot1;

        levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null && levelManager.isInTutorial())
        {
            reverseCamera = -1;
        } else
        {
            reverseCamera = 1;
        }
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

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        horizontalAngle += mouseX * sensitivity * Time.deltaTime;

        // Update vertical angle only if it doesn't exceed limits
        float newVerticalAngle = verticalAngle - mouseY * sensitivity * Time.deltaTime;
        verticalAngle = Mathf.Clamp(newVerticalAngle, minVerticalAngle, maxVerticalAngle);

        Quaternion rotation = Quaternion.Euler(verticalAngle * reverseCamera, horizontalAngle, 0);

        Vector3 offset = rotation * new Vector3(0, 0, -currentZoomDistance * reverseCamera);
        cam.transform.position = currentPivot.position + offset;

        cam.transform.LookAt(currentPivot.position);
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
