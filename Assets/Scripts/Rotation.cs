using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float bounceAmplitude = 0.2f;
    public float bounceFrequency = 1f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (gameObject.CompareTag("Button"))
        {
            transform.Rotate(0f, 60f * Time.deltaTime, 0f, Space.Self);

            float newY = initialPosition.y + Mathf.Sin(Time.time * bounceFrequency) * bounceAmplitude;
            transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
        }
        else if (gameObject.CompareTag("Wings"))
        {
            transform.Rotate(0f, 15f * Time.deltaTime, 0f, Space.Self);
        }
        else if (gameObject.CompareTag("Rotation"))
        {
            transform.Rotate(0f, 30f * Time.deltaTime, 0f, Space.Self);
        }
    }
}