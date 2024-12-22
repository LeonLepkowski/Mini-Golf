using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 leftBoundary;
    private Vector3 rightBoundary;
    private bool movingRight = true;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        leftBoundary = startPosition - transform.right * 11f;
        rightBoundary = startPosition + transform.right * 11f;
    }

    void FixedUpdate()
    {
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