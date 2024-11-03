using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public static Levels Instance { get; private set; }
    public TMP_Text level;
    private int levelIndex = 1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            LoadLevel();
        }
    }

    private void Start()
    {
        UpdateLevel();
    }

    public void LoadLevel()
    {
        levelIndex = PlayerPrefs.GetInt("HighScore", 1);
        UpdateLevel();
    }

    public void UpdateLevel()
    {
        if (level != null)
        {
            level.text = levelIndex.ToString();
        }
    }

    public void SaveLevel()
    {
        PlayerPrefs.SetInt("HighScore", levelIndex);
        PlayerPrefs.Save();
        levelIndex++;
    }

    public void ResetLevel()
    {
        levelIndex = 1;
        PlayerPrefs.SetInt("HighScore", levelIndex);
        PlayerPrefs.Save();
        UpdateLevel();
    }

    public void CompleteLevel()
    {
        levelIndex++;
        SaveLevel();
        UpdateLevel();
    }

    public void GameStart()
    {
        string levelName = "level" + levelIndex;
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
