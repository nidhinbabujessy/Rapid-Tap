using UnityEngine;
using TMPro; // Make sure to include this if you are using TextMeshPro for UI

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }  // Singleton instance
    private int score;                                         // Player's score
    public TMP_Text scoreText;                               // Reference to the UI text element

    private void Awake()
    {
        // Check if an instance of ScoreManager already exists
        if (Instance == null)
        {
            Instance = this; // Assign this instance to the static variable
            DontDestroyOnLoad(gameObject); // Keep this instance alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        UpdateScoreDisplay(); // Update the UI at the start
    }

    // Method to increase score
    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreDisplay(); // Update the UI display
    }

    // Method to decrease score
    public void DecreaseScore(int amount)
    {
        score -= amount;
        UpdateScoreDisplay(); // Update the UI display
    }

    // Method to update the score display
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score; // Update the score text
        }
    }

    // Method to get the current score
    public int GetScore()
    {
        return score;
    }
}
