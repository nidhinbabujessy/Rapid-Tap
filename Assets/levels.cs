using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public static Levels Instance { get; private set; }  // Singleton instance
    public TMP_Text level;
    private int levelIndex = 1;

    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  // Destroy duplicate instances
        }
        else
        {
            Instance = this;  // Set this as the singleton instance
           // DontDestroyOnLoad(gameObject);  // Persist across scenes
            LoadLevel();  // Load saved level index
        }
    }

    private void Start()
    {
        UpdateLevel();
    }

    // Loads the saved level index from PlayerPrefs
    public void LoadLevel()
    {
        levelIndex = PlayerPrefs.GetInt("HighScore", 1);  // Default to 1 if no saved level
        Debug.Log("Loaded High Score Level: " + levelIndex);
        UpdateLevel();
    }

    // Updates the level text display
    public void UpdateLevel()
    {
        if (level != null)
        {
            level.text = levelIndex.ToString();
        }
    }

    // Saves the current level index to PlayerPrefs
    public void SaveLevel()
    {
        PlayerPrefs.SetInt("HighScore", levelIndex);
        PlayerPrefs.Save();
        levelIndex++;
        Debug.Log("High Score Saved: " + levelIndex);
    }

    // Resets the level index and reloads the home scene
    public void ResetLevel()
    {
        levelIndex = 1;  // Reset to the initial level
        PlayerPrefs.SetInt("HighScore", levelIndex);  // Update PlayerPrefs
        PlayerPrefs.Save();  // Save changes
        UpdateLevel();  // Update the displayed level text
        //SceneManager.LoadScene("home");
    }

    public void CompleteLevel()
    {
        levelIndex++;
        SaveLevel();  // Save progress
        UpdateLevel();  // Update level display
    }

    // Method to start the game by loading the corresponding level scene
    public void GameStart()
    {
        int index = levelIndex ;
        string levelName = "level" + index;
        if (Application.CanStreamedLevelBeLoaded(levelName))
        {
            SceneManager.LoadScene(levelName);
        }
        else
        {
            Debug.LogWarning("No more levels available.");
        }
    }
}
