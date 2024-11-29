using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public float speed = 2f; // Speed of the platform
    private Vector3 leftBoundary; // Left boundary
    private Vector3 rightBoundary; // Right boundary
    private bool movingRight = true; // Direction flag
    private Vector3 startPosition; // Starting position of the platform

    void Start()
    {
        startPosition = transform.position;
        leftBoundary = startPosition - transform.right * 11f;
        rightBoundary = startPosition + transform.right * 11f;
    }

    void Update()
    {
        // Move the platform
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, rightBoundary) < 0.1f)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, leftBoundary) < 0.1f)
            {
                movingRight = true;
            }
        }
    }
}