using UnityEngine;
using System.Collections;

public class WorldSwitcher : MonoBehaviour
{
    public GameObject world1;
    public GameObject world2;
    public Material skybox1;
    public Material skybox2;
    public FadeController fadeController;
    public AudioClip portalSoundEffect;
    private AudioSource audioSource;
    private bool isWorld1Active = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        world1.SetActive(isWorld1Active);
        world2.SetActive(!isWorld1Active);
        RenderSettings.skybox = isWorld1Active ? skybox1 : skybox2;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(SwitchWorlds());
        }
    }

    IEnumerator SwitchWorlds()
    {
        if (fadeController != null)
        {
            if (portalSoundEffect != null && audioSource != null)
            {
                audioSource.PlayOneShot(portalSoundEffect);
            }
            yield return StartCoroutine(fadeController.FadeIn());
        }

        isWorld1Active = !isWorld1Active;
        world1.SetActive(isWorld1Active);
        world2.SetActive(!isWorld1Active);
        RenderSettings.skybox = isWorld1Active ? skybox1 : skybox2;

        if (fadeController != null)
        {
            yield return StartCoroutine(fadeController.FadeOut());
        }
    }
}