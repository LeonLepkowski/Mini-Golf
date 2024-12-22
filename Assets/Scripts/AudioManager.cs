using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic;
    private AudioSource audioSource;

    private static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;

            float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
            SetVolume(savedVolume);

            PlayBackgroundMusic();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Background music clip is not assigned.");
        }
    }

    public void StopBackgroundMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}