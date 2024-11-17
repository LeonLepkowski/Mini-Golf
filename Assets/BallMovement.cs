using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    public float maxForce = 50f;
    public Slider forceSlider;
    private Rigidbody rb;
    private Vector3 aimDirection;
    private float holdTime;

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
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            holdTime = 0f;
        }

        if (Input.GetMouseButton(0))
        {
            holdTime += Time.deltaTime;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                aimDirection = (hit.point - transform.position).normalized;
            }

            if (forceSlider != null && rb.linearVelocity.magnitude < 0.1f)
            {
                forceSlider.value = Mathf.Clamp(holdTime * maxForce, 0, maxForce);
            }
        }

        if (Input.GetMouseButtonUp(0) && rb.linearVelocity.magnitude < 0.1f)
        {
            float force = Mathf.Clamp(holdTime * maxForce, 0, maxForce);
            rb.AddForce(aimDirection * force, ForceMode.Impulse);

            if (forceSlider != null)
            {
                forceSlider.value = 0f;
            }
        }

        if (forceSlider != null)
        {
            forceSlider.interactable = rb.linearVelocity.magnitude < 0.1f;
        }
    }
}