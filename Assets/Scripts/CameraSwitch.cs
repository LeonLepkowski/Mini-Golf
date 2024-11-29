using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera camera1; // First camera
    public Camera camera2; // Second camera
    private int currentCameraIndex = 0; // Index of the current camera

    void Start()
    {
        // Ensure only the first camera is active at the start
        camera1.gameObject.SetActive(true);
        camera2.gameObject.SetActive(false);
        Debug.Log("CameraFollow script started. Initial camera set to camera1.");
    }

    void Update()
    {
        // Switch cameras when "C" key is pressed
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Switching cameras...");
            SwitchCamera();
        }
    }
    void SwitchCamera()
    {
        // Deactivate the current camera
        if (currentCameraIndex == 0)
        {
            camera1.gameObject.SetActive(false);
            camera2.gameObject.SetActive(true);
        }
        else
        {
            camera1.gameObject.SetActive(true);
            camera2.gameObject.SetActive(false);
        }

        // Toggle the camera index
        currentCameraIndex = 1 - currentCameraIndex;
        Debug.Log("Switched to camera " + (currentCameraIndex + 1));
    }
}