using UnityEngine;

public class WorldSwitcher : MonoBehaviour
{
    public GameObject world1;
    public GameObject world2;
    private bool isWorld1Active = true;

    void Start()
    {
        // Ensure only one world is active at the start
        world1.SetActive(isWorld1Active);
        world2.SetActive(!isWorld1Active);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchWorlds();
        }
    }

    void SwitchWorlds()
    {
        isWorld1Active = !isWorld1Active;
        world1.SetActive(isWorld1Active);
        world2.SetActive(!isWorld1Active);
    }
}