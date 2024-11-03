using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }  // Singleton instance
    private int score;                                         // Player's score
    public TMP_Text scoreText;
    public TMP_Text FinalScore1;
    public TMP_Text FinalScore2; // Final score display
    public TMP_Text timerText;                                 // Timer display
    public float level;
    public string NextLevel;
    public bool spawn = true;
    private float timer = 60f;                                 // 1-minute timer in seconds
    public GameObject Win;
    public GameObject fail;
    private bool timers;

    private void Awake()
    {
        spawn = true;
        timers = true;

        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        reasigning();
        UpdateScoreDisplay(); // Update the score UI at the start
        UpdateTimerDisplay(); // Initialize the timer display
    }

    void reasigning()
    {
        if (scoreText == null) scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
        if (timerText == null) timerText = GameObject.Find("Timer").GetComponent<TMP_Text>();
        if (FinalScore1 == null) FinalScore1 = GameObject.Find("FinalScore1").GetComponent<TMP_Text>();
        if (FinalScore2 == null) FinalScore2 = GameObject.Find("FinalScore2").GetComponent<TMP_Text>();
        if (Win == null) Win = GameObject.Find("win");
        if (fail == null) fail = GameObject.Find("fail");
    }

    private void Update()
    {
        // Update timer only if active and score is positive
        if (timer > 0 && score >= 0)
        {
            timer -= Time.deltaTime; // Decrease timer by frame time
            UpdateTimerDisplay();

            // Check if time has run out
            if (timer <= 0)
            {
                timer = 0; // Clamp timer to 0
                OnTimerEnd(); // Handle end of timer
            }
        }
    }

    // Method to increase score
    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreDisplay(); // Update score UI
    }

    // Method to decrease score
    public void DecreaseScore(int amount)
    {
        score -= amount;
        UpdateScoreDisplay(); // Update score UI

        if (score < 0 && timers) // Trigger fail condition if score is below 0
        {
            failMethod();
        }
    }

    // Update score UI
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); // Display score
        }
    }

    // Update timer UI
    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int seconds = Mathf.CeilToInt(timer);
            timerText.text = "Time: " + seconds; // Display as "Time: X"
        }
    }

    // Handle timer end
    private void OnTimerEnd()
    {
        if (score > 0) // Trigger win condition if score is positive
        {
            Levels.Instance.CompleteLevel();
            Win.SetActive(true);
            spawn = false;
            FinalScore1.text = score.ToString();
        }
        else // Trigger fail condition if score is zero or below
        {
            failMethod();
        }

        Debug.Log("Timer has ended!");
    }

    // Method to handle fail condition
    public void failMethod()
    {
        if (timers) // Ensure only one end condition is triggered
        {
            fail.SetActive(true);
            timers = false;
            spawn = false;
            FinalScore2.text = score.ToString(); // Display final score
        }
    }

    // Method to go to home scene and update level
    public void GoHome()
    {
        SceneManager.sceneLoaded += OnHomeSceneLoaded;  // Add listener for scene load
        SceneManager.LoadScene("home");                 // Load home scene
    }

    // Called after the home scene is loaded
    private void OnHomeSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "home")
        {
            Levels.Instance?.LoadLevel(); // Call UpdateLevel in Levels instance
            SceneManager.sceneLoaded -= OnHomeSceneLoaded; // Remove listener
        }
    }

    // Reload the current scene and reset values
    public void ReloadScene()
    {
        timers = true;
        score = 0;                       // Reset score
        timer = 60f;                      // Reset timer
        spawn = true;                     // Allow spawning again
        Win.SetActive(false);             // Hide win UI
        fail.SetActive(false);            // Hide fail UI
        FinalScore1.text = "";
        FinalScore2.text = "";
        UpdateScoreDisplay();             // Reset score display
        UpdateTimerDisplay();             // Reset timer display
    }

    // Proceed to the next level
    public void nextScene()
    {
        SceneManager.LoadScene(NextLevel);
    }
}
