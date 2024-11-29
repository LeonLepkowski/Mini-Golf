using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The ball to follow
    public float distance = 5.0f; // Distance from the ball
    public float height = 2.0f; // Height above the ball
    public float rotationSpeed = 2.0f; // Speed of rotation

    private float currentRotationAngle;
    private float currentHeight;
    private Quaternion currentRotation;

    void LateUpdate()
    {
        if (target == null)
            return;

        // Calculate the current rotation angle based on input
        currentRotationAngle += Input.GetAxis("Horizontal") * rotationSpeed;

        // Convert the angle into a rotation
        currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Calculate the desired position
        Vector3 position = target.position - (currentRotation * Vector3.forward * distance);
        position.y = target.position.y + height;

        // Set the position and rotation of the camera
        transform.position = position;
        transform.LookAt(target);
    }
}