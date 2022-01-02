using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        PauseMenu.isPaused = false;
    }

    public void PlayGame() => LevelManager.LoadNextLevel();

    public void Menu() => LevelManager.LoadMenuLevel();

    public void QuitGame() => Application.Quit();
}
