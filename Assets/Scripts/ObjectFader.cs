using UnityEngine;

public class ObjectFader : MonoBehaviour
{
    public float fadeSpeed, fadeAmmount;
    float originalOpacity;
    Material[] Mats;
    public bool DoFade = false;
    void Start()
    {
        Mats = GetComponent<Renderer>().materials;
        foreach (Material mat in Mats)
        {
            originalOpacity = mat.color.a;
        }
    }

    void Update()
    {
        if (DoFade)
        {
            FadeNow();
        }
        else
        {
            ResetFade();
        }
    }

    void FadeNow()
    {
        foreach (Material mat in Mats)
        {
            Color currentColor = mat.color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b,
                Mathf.Lerp(currentColor.a, fadeAmmount, fadeSpeed * Time.deltaTime));
            mat.color = smoothColor;
        }
    }

    void ResetFade()
    {
        foreach (Material mat in Mats)
        {
            Color currentColor = mat.color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b,
                Mathf.Lerp(currentColor.a, originalOpacity, fadeSpeed * Time.deltaTime));
            mat.color = smoothColor;
        }

    }
}
