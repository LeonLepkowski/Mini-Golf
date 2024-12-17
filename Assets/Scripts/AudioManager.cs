using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic; // Assign this in the Unity Editor
    private AudioSource audioSource;

    private static AudioManager instance;

    void Awake()
    {
        // Ensure only one instance of AudioManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Add an AudioSource component to the GameObject
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true; // Set the AudioSource to loop

            // Load the saved volume setting
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
        PlayerPrefs.SetFloat("MusicVolume", volume); // Save the volume setting
    }
}