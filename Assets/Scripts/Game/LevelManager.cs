using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Singleton

    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    // Current level number in the game
    public static int levelNumber = 0;

    // Number of enemies on current level
    public int enemiesCount;

    public static void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public static void LoadNextLevel()
    {
        // Reset level number when in the menu
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            levelNumber = 0;
        }

        levelNumber++;
        SceneManager.LoadScene(levelNumber);
    }

    public static void LoadMenuLevel() => SceneManager.LoadScene("Menu");

    private void Start()
    {
        // At the start of each level get the number of enemies
        enemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    private void Update()
    {
        // Load the next level if all the enemies are dead and current scene is a level
        if (enemiesCount == 0 && SceneManager.GetActiveScene().name.StartsWith("Scene"))
        {
            LoadNextLevel();
        }
    }
}
