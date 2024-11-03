using UnityEngine;

public class CircleObject : MonoBehaviour
{
    public float minX = -1.5f;         // Minimum X position for repositioning
    public float maxX = 1.5f;          // Maximum X position for repositioning
    public float fixedY = 4f;          // Fixed Y position for repositioning
    private float moveDownSpeed;       // Speed at which the object moves down
    private Rigidbody2D rb;            // Reference to the Rigidbody2D component
    public bool enemy;                 // Determines if the object is an enemy

    private AudioSource audioSource;
    public AudioClip stone;
    public AudioClip bubble;           // Reference to the AudioSource component

    private void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Set Rigidbody2D to kinematic if not using physics
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Set moveDownSpeed based on the level from ScoreManager
        if (ScoreManager.Instance.level == 1)
        {
            moveDownSpeed = 2;
        }
        else if (ScoreManager.Instance.level == 2)
        {
            moveDownSpeed = 3;
        }
        else if (ScoreManager.Instance.level == 3)
        {
            moveDownSpeed = 4;
        }
        else if (ScoreManager.Instance.level == 4)
        {
            moveDownSpeed = 5;
        }
        else if (ScoreManager.Instance.level == 5)
        {
            moveDownSpeed = 6;
        }
    }

    private void Update()
    {
        // Move the object downwards over time
        transform.position += Vector3.down * moveDownSpeed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        // Respawn the object if it goes off-screen and spawning is allowed
        if (ScoreManager.Instance.spawn)
        {
            Spawn();
        }
    }

    private void OnMouseDown()
    {
        // Play the correct audio clip based on the object's tag
        if (audioSource != null)
        {
            if (gameObject.CompareTag("bubble"))
            {
                audioSource.clip = bubble;
            }
            else if(gameObject.CompareTag("stone"))
            {
                audioSource.clip = stone;
            }
            audioSource.Play();
        }

        // Respawn the object and update the score
        Spawn();
        UpdateScore();
    }

    void Spawn()
    {
        float randomX = Random.Range(minX, maxX);

        // Reset the velocity and position to avoid unwanted movement
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }

        // Set the object's position to the new random X and fixed Y
        transform.position = new Vector3(randomX, fixedY, 0);
    }

    void UpdateScore()
    {
        if (ScoreManager.Instance != null)
        {
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
}
