using UnityEngine;

public class Mill : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0f, 15f * Time.deltaTime, 0f, Space.Self);
    }
}
