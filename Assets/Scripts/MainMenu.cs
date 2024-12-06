using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Reset UnlockedLevels
    // public void Start() {
    //     PlayerPrefs.DeleteAll();        
    // }

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