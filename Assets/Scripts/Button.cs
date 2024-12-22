using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject door;
    public Vector3 doorOpenPosition;
    public float moveSpeed = 1f;

    private bool isTriggered = false;

    public AudioClip collectSoundEffect;
    private AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            isTriggered = true;
        }
        if (collectSoundEffect != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSoundEffect);
        }
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    private void FixedUpdate()
    {
        if (isTriggered)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, doorOpenPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
}