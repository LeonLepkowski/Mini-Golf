using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float height = 2.0f;
    public float rotationSpeed = 2.0f;
    private float currentRotationAngle;
    private float currentHeight;
    private Quaternion currentRotation;

    void LateUpdate()
    {
        if (target == null)
            return;

        if (Input.GetMouseButton(1))
        {
            currentRotationAngle += Input.GetAxis("Mouse X") * rotationSpeed;
        }

        currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        Vector3 position = target.position - (currentRotation * Vector3.forward * distance);
        position.y = target.position.y + height;

        transform.position = position;
        transform.LookAt(target);
    }
}