using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    public float maxForce = 50f;
    public Slider forceSlider; // Reference to the UI Slider
    private Rigidbody rb;
    private Vector3 aimDirection;
    private float holdTime;
    private Vector3 initialMousePosition;
    private bool isDragging;
    private bool isInitialClickOnBall;
    private LineRenderer lineRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.linearDamping = 1f;
        rb.angularDamping = 1f;

        if (forceSlider != null)
        {
            forceSlider.minValue = 0f;
            forceSlider.maxValue = maxForce;
            forceSlider.value = 0f;
            forceSlider.interactable = true;
        }

        // Initialize the LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.3f; // Wider line
        lineRenderer.endWidth = 0.1f;   // Wider line
        lineRenderer.positionCount = 0;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white; // White color
        lineRenderer.endColor = Color.white;   // White color
    }

    void Update()
    {
        // Cache the ball's velocity magnitude
        float velocityMagnitude = rb.linearVelocity.magnitude;

        if (Input.GetMouseButtonDown(0))
        {
            holdTime = 0f;
            initialMousePosition = Input.mousePosition;

            // Perform a raycast to check if the initial click is on the ball
            Ray ray = Camera.main.ScreenPointToRay(initialMousePosition);
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
            Ray initialRay = Camera.main.ScreenPointToRay(initialMousePosition);
            Ray currentRay = Camera.main.ScreenPointToRay(currentMousePosition);
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

            // Update the Slider value based on drag distance
            if (forceSlider != null && velocityMagnitude < 0.1f)
            {
                forceSlider.value = clampedForce;

                // Update the slider position to indicate the trajectory
                Vector3 predictedPosition = transform.position + (-aimDirection * clampedForce * 0.25f); // Shorter line
                forceSlider.transform.position = Camera.main.WorldToScreenPoint(predictedPosition);
            }

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

            // Reset the Slider value
            if (forceSlider != null)
            {
                forceSlider.value = 0f;
            }

            // Clear the trajectory line
            lineRenderer.positionCount = 0;
        }

        // Disable slider interaction while the ball is moving
        if (forceSlider != null)
        {
            forceSlider.interactable = velocityMagnitude < 0.1f;
        }
    }
}