using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class BallMovement : MonoBehaviour
{
    public float maxForce = 50f;
    private Rigidbody rb;
    private Vector3 aimDirection;
    private float holdTime;
    private Vector3 initialMousePosition;
    private bool isDragging;
    private bool isInitialClickOnBall;
    private LineRenderer lineRenderer;

    public TextMeshProUGUI moveCountText;
    public TextMeshProUGUI highScoreText;
    private int moveCount;
    private int highScore;
    private string sceneName;

    public AudioClip moveSoundEffect; // Assign this in the Unity Editor
    public AudioClip holeSoundEffect; // Assign this in the Unity Editor
    private AudioSource audioSource;

    void Start()
    {
        moveCount = 0;
        sceneName = SceneManager.GetActiveScene().name;
        highScore = PlayerPrefs.GetInt(sceneName + "_HighScore", int.MaxValue);
        UpdateUI();

        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing from the ball.");
            return;
        }

        rb.linearDamping = 1f;
        rb.angularDamping = 1f;

        // Initialize the LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.3f; // Wider line
        lineRenderer.endWidth = 0.1f;   // Wider line
        lineRenderer.positionCount = 0;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white; // White color
        lineRenderer.endColor = Color.white;   // White color

        // Initialize the AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (rb == null)
            return;

        // Cache the ball's velocity magnitude
        float velocityMagnitude = rb.linearVelocity.magnitude;

        if (Input.GetMouseButtonDown(0))
        {
            holdTime = 0f;
            initialMousePosition = Input.mousePosition;

            // Perform a raycast to check if the initial click is on the ball
            Ray ray = GetActiveCamera().ScreenPointToRay(initialMousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isInitialClickOnBall = true;
                    isDragging = true;
                }
                else
                {
                    isInitialClickOnBall = false;
                }
            }
        }

        if (Input.GetMouseButton(0) && isDragging && isInitialClickOnBall)
        {
            holdTime += Time.deltaTime;

            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 dragVector = currentMousePosition - initialMousePosition;

            // Convert screen space drag vector to world space
            Ray initialRay = GetActiveCamera().ScreenPointToRay(initialMousePosition);
            Ray currentRay = GetActiveCamera().ScreenPointToRay(currentMousePosition);
            Plane plane = new Plane(Vector3.up, transform.position);
            float initialDistance, currentDistance;
            plane.Raycast(initialRay, out initialDistance);
            plane.Raycast(currentRay, out currentDistance);
            Vector3 worldInitialPosition = initialRay.GetPoint(initialDistance);
            Vector3 worldCurrentPosition = currentRay.GetPoint(currentDistance);
            aimDirection = (worldCurrentPosition - worldInitialPosition).normalized;

            // Calculate the clamped force based on drag distance
            float dragDistance = dragVector.magnitude;
            float clampedForce = Mathf.Clamp(dragDistance, 0, maxForce);

            // Update the trajectory line
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + (-aimDirection * clampedForce * 0.15f)); // Shorter line
        }

        if (Input.GetMouseButtonUp(0) && isDragging && isInitialClickOnBall)
        {
            isDragging = false;

            // Calculate the clamped force based on drag distance
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 dragVector = currentMousePosition - initialMousePosition;
            float dragDistance = dragVector.magnitude;
            float clampedForce = Mathf.Clamp(dragDistance, 0, maxForce);

            // Invert the direction and scale down the force
            rb.AddForce(-aimDirection * clampedForce * 0.5f, ForceMode.Impulse);

            // Play the move sound effect
            if (moveSoundEffect != null && audioSource != null)
            {
                audioSource.PlayOneShot(moveSoundEffect);
            }

            // Increment the move count
            moveCount++;
            UpdateUI();

            // Clear the trajectory line
            lineRenderer.positionCount = 0;
        }
    }

    private Camera GetActiveCamera()
    {
        // Return the currently active camera
        if (Camera.main != null && Camera.main.isActiveAndEnabled)
        {
            return Camera.main;
        }

        Camera[] cameras = Camera.allCameras;
        foreach (Camera cam in cameras)
        {
            if (cam.isActiveAndEnabled)
            {
                return cam;
            }
        }

        Debug.LogError("No active camera found!");
        return null;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("LevelExit"))
        {
            Debug.Log("Collision with LevelExit detected.");

            if (holeSoundEffect != null && audioSource != null)
            {
                audioSource.PlayOneShot(holeSoundEffect);
                StartCoroutine(WaitAndLoadScene(1.0f)); // Wait for 1 second before loading the next scene
            }

            if (moveCount < highScore)
            {
                highScore = moveCount;
                PlayerPrefs.SetInt(sceneName + "_HighScore", highScore);
                PlayerPrefs.Save();
            }
            moveCount = 0;
            UpdateUI();

            Debug.Log("Level Complete!");
            UnlockNewLevel();
        }
    }

    private IEnumerator WaitAndLoadScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(0);
    }

    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeathZone"))
        {
            moveCount = 0;
            UpdateUI();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void UpdateUI()
    {
        moveCountText.text = "Moves: " + moveCount;
        highScoreText.text = "High Score: " + (highScore == int.MaxValue ? "N/A" : highScore.ToString());
    }
}