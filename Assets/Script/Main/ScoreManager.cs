using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    #region Serialized Fields

    [Header("Score Display")]
    [SerializeField] private int score;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text FinalScore1;
    [SerializeField] private TMP_Text FinalScore2;

    [Header("Timer Settings")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float timer = 60f;

    [Header("Level Settings")]
    public float level;
    [SerializeField] private string NextLevel;

    [Header("Game State UI")]
    [SerializeField] private GameObject Win;
    [SerializeField] private GameObject fail;

    [Header("Game Control Flags")]
    public bool spawn = true;
    private bool timers;

    #endregion

    #region Unity Lifecycle Methods

    private void Awake()
    {
        spawn = true;
        timers = true;

        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Reasigning();
        UpdateScoreDisplay();
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (timer > 0 && score >= 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerDisplay();

            if (timer <= 0)
            {
                timer = 0;
                OnTimerEnd();
            }
        }
    }

    #endregion

    #region UI Update Methods

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int seconds = Mathf.CeilToInt(timer);
            timerText.text = "Time: " + seconds;
        }
    }

    #endregion

    #region Score Management

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreDisplay();
    }

    public void DecreaseScore(int amount)
    {
        score -= amount;
        UpdateScoreDisplay();

        if (score < 0 && timers)
        {
            FailMethod();
        }
    }

    #endregion

    #region Timer End Handling

    private void OnTimerEnd()
    {
        if (score > 0)
        {
            Levels.Instance.CompleteLevel();
            Win.SetActive(true);
            spawn = false;
            FinalScore1.text = score.ToString();
        }
        else
        {
            FailMethod();
        }
    }

    #endregion

    #region Game End Methods

    public void FailMethod()
    {
        if (timers)
        {
            fail.SetActive(true);
            timers = false;
            spawn = false;
            FinalScore2.text = score.ToString();
        }
    }

    #endregion

    #region Scene Management

    public void GoHome()
    {
        SceneManager.sceneLoaded += OnHomeSceneLoaded;
        SceneManager.LoadScene("home");
    }

    private void OnHomeSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "home")
        {
            Levels.Instance?.LoadLevel();
            SceneManager.sceneLoaded -= OnHomeSceneLoaded;
        }
    }

    public void ReloadScene()
    {
        timers = true;
        score = 0;
        timer = 60f;
        spawn = true;
        Win.SetActive(false);
        fail.SetActive(false);
        FinalScore1.text = "";
        FinalScore2.text = "";
        UpdateScoreDisplay();
        UpdateTimerDisplay();
    }

    public void NextScene()
    {
        SceneManager.LoadScene(NextLevel);
    }

    #endregion

    #region Helper Methods

    private void Reasigning()
    {
        if (scoreText == null) scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
        if (timerText == null) timerText = GameObject.Find("Timer").GetComponent<TMP_Text>();
        if (FinalScore1 == null) FinalScore1 = GameObject.Find("FinalScore1").GetComponent<TMP_Text>();
        if (FinalScore2 == null) FinalScore2 = GameObject.Find("FinalScore2").GetComponent<TMP_Text>();
        if (Win == null) Win = GameObject.Find("win");
        if (fail == null) fail = GameObject.Find("fail");
    }

    #endregion
}
