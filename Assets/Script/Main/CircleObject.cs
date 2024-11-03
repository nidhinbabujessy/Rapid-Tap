using UnityEngine;

public class CircleObject : MonoBehaviour
{
    [Header("Position Settings")]
    [SerializeField] private float minX = -1.5f;    // Minimum X position for repositioning
    [SerializeField] private float maxX = 1.5f;     // Maximum X position for repositioning
    [SerializeField] private float fixedY = 4f;     // Fixed Y position for repositioning

    [Header("Movement Settings")]
    [SerializeField] private float moveDownSpeed;   // Speed at which the object moves down

    [Header("Enemy Settings")]
    [SerializeField] private bool enemy;            // Determines if the object is an enemy

    [Header("Audio Settings")]
    [SerializeField] private AudioClip stone;
    [SerializeField] private AudioClip bubble;

    private Rigidbody2D rb;
    private AudioSource audioSource;

    private void Start()
    {
        #region Initialization
        rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.isKinematic = true;

        audioSource = GetComponent<AudioSource>();

        // Set moveDownSpeed based on the level from ScoreManager
        float level = ScoreManager.Instance.level;
        moveDownSpeed = Mathf.Clamp(level + 1, 2, 6);
        #endregion
    }

    private void Update()
    {
        // Move the object downwards over time
        transform.position += Vector3.down * moveDownSpeed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        if (ScoreManager.Instance.spawn)
        {
            Spawn();
        }
    }

    private void OnMouseDown()
    {
        PlayAudio();
        Spawn();
        UpdateScore();
    }

    private void PlayAudio()
    {
        if (audioSource == null) return;

        if (gameObject.CompareTag("bubble"))
        {
            audioSource.clip = bubble;
        }
        else if (gameObject.CompareTag("stone"))
        {
            audioSource.clip = stone;
        }

        audioSource.Play();
    }

    private void Spawn()
    {
        float randomX = Random.Range(minX, maxX);
        if (rb != null) rb.velocity = Vector2.zero;

        transform.position = new Vector3(randomX, fixedY, 0);
    }

    private void UpdateScore()
    {
        if (ScoreManager.Instance == null) return;

        if (enemy)
        {
            ScoreManager.Instance.DecreaseScore(5);
        }
        else
        {
            ScoreManager.Instance.IncreaseScore(5);
        }
    }
}
