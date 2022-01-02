using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // czy ispaused to dobra nazwa (bo potem jest PauseMenu.isPaused)
    public static bool isPaused = false;

    public GameObject Pause;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // When escape key pressed enter or exit pause menu
            isPaused = !isPaused;
        }
        // When paused show pause panel and stop time
        if (isPaused)
        {
            Pause.SetActive(true);
            Time.timeScale = 0f;
        }
        // When unpaused deactivate pause panel
        else
        {
            Pause.SetActive(false);
        }
    }
    public void Resume()
    {
        isPaused = false;
    }
    public void Menu()
    {
        isPaused = false;
        LevelManager.LoadMenuLevel();
    }
    public void NextLevel()
    {
        isPaused = false;
        LevelManager.LoadNextLevel();
    }
    
}
