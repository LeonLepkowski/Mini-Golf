using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject door;
    public Vector3 doorOpenPosition;
    public float moveSpeed = 1f;

    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            isTriggered = true;
        }
        GetComponent<Renderer>().enabled = false;
    }

    private void Update()
    {
        if (isTriggered)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, doorOpenPosition, moveSpeed * Time.deltaTime);
        }
    }
}