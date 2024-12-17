using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Reset UnlockedLevels
    // public void Start() {
    //     PlayerPrefs.DeleteAll();        
    // }

    public Slider volumeSlider;
    private AudioManager audioManager;

    void Start()
    {
        InitializeVolumeSlider();
    }

    private void OnEnable()
    {
        InitializeVolumeSlider();
    }

    private void InitializeVolumeSlider()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null && volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
            volumeSlider.onValueChanged.AddListener(audioManager.SetVolume);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenLevel(int levelID)
    {
        string levelName = "Level " + levelID;
        SceneManager.LoadScene(levelName);
    }

    public Button[] buttons;

    private void Awake()
    {
        int UnlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < UnlockedLevel && i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
    }

}