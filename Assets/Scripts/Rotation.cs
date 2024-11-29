using UnityEngine;

public class Rotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0f, 30f * Time.deltaTime, 0f, Space.Self);
    }
}
