using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }  // Singleton instance
    private int score;                                         // Player's score
    public TMP_Text scoreText;
    public TMP_Text FinalSCore;// Reference to the score UI text element
    public TMP_Text timerText;                                 // Reference to the timer UI text element
    public float level;
    public bool spawn = true;
    private float timer = 60f; // 1-minute timer in seconds
    public GameObject PopUp;

    private void Awake()
    {
        spawn = true;
        PopUp.SetActive(false);
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
        UpdateTimerDisplay(); // Initialize the timer display
    }

    private void Update()
    {
        // Decrease timer only if it's greater than zero
        if (timer > 0)
        {
            timer -= Time.deltaTime; // Decrease timer by the time since the last frame
            UpdateTimerDisplay();

            // Check if time has run out
            if (timer <= 0)
            {
                timer = 0; // Clamp timer to 0
                OnTimerEnd(); // Call the method to handle end of timer
            }
        }
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
            scoreText.text = score.ToString(); // Update the score text
        }
    }

    // Method to update the timer display
    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int seconds = Mathf.CeilToInt(timer); // Get the remaining seconds
            timerText.text = "Time: " + seconds;  // Display as "Time: X"
        }
    }

    // Method called when the timer ends 
    private void OnTimerEnd()
    {
        PopUp.SetActive(true);
        // Add logic here for what should happen when the timer ends, e.g., end the game, stop score updates
        Debug.Log("Timer has ended!");
        spawn = false;
        FinalSCore.text = score.ToString();
    }

    public void ReloadScene()
    {
        // Reload the current scene
        score = 0;                       // Reset score
        timer = 60f;                      // Reset timer
        spawn = true;                     // Allow spawning again
        PopUp.SetActive(false);           // Hide the pop-up
        UpdateScoreDisplay();             // Reset the score display
        UpdateTimerDisplay();             // Reset the timer display

    }
    // Method to get the current score
    public int GetScore()
    {
        return score;
    }
}