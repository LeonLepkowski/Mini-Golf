using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitGame() {
        Application.Quit();
    }

    public void OpenLevel(int levelID) {
        string levelName = "Level " + levelID;
        SceneManager.LoadScene(levelName);
    }
}
