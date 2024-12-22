using UnityEngine;

public class GravityChange : MonoBehaviour
{
    private Vector3 defaultGravity;
    private Vector3 newGravity = new Vector3(0, 20f, 0);

    void Start()
    {
        // Store the default gravity
        defaultGravity = Physics.gravity;
        Debug.Log("Default gravity: " + defaultGravity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("G key pressed");
            if (Physics.gravity == defaultGravity)
            {
                Physics.gravity = newGravity;
                Debug.Log("Gravity changed to: " + newGravity);
            }
            else
            {
                Physics.gravity = defaultGravity;
                Debug.Log("Gravity reset to: " + defaultGravity);
            }
        }
    }
}