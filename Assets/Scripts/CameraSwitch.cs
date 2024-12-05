using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSwitch : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    private int currentCameraIndex = 0;

    void Start()
    {
        camera1.gameObject.SetActive(true);
        camera2.gameObject.SetActive(false);
        Debug.Log("CameraFollow script started. Initial camera set to camera1.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Switching cameras...");
            SwitchCamera();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Returning to the main menu...");
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Restarting the game...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    void SwitchCamera()
    {
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

        currentCameraIndex = 1 - currentCameraIndex;
        Debug.Log("Switched to camera " + (currentCameraIndex + 1));
    }
}