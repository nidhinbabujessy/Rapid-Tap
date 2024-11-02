using UnityEngine;

public class CircleObject : MonoBehaviour
{
   // public static CircleObject instance { get; private set; }
    public float minX = -1.5f;         // Minimum X position for repositioning
    public float maxX = 1.5f;          // Maximum X position for repositioning
    public float fixedY = 4f;          // Fixed Y position for repositioning
    private float moveDownSpeed;   // Speed at which the object moves down

    private Rigidbody2D rb;  // Reference to the Rigidbody2D component
    public bool enemy;

   
    private void Start()
    {
       // moveDownSpeed = RandomSpawner.Instance.SpeedOfObject;
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component

        // Optionally set Rigidbody2D to kinematic if not using physics
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        if(ScoreManager.Instance.level ==1)
        {
            moveDownSpeed = 2;
        }
        else if(ScoreManager.Instance.level ==2)
        {
            moveDownSpeed = 3;
        }
        else if (ScoreManager.Instance.level ==3)
            { moveDownSpeed = 4; }
        else if (ScoreManager.Instance.level ==4)
        { moveDownSpeed= 5; }
        else if (ScoreManager.Instance.level==5)
        { moveDownSpeed= 6; }
    }

    private void Update()
    {
        // Move the object downwards over time
        transform.position += Vector3.down * moveDownSpeed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        // Respawn the object if it goes off-screen
        if (ScoreManager.Instance.spawn == true) { Spawn(); }
    }

    private void OnMouseDown()
    {
        Spawn();
        UpdateScore();
    }

    void Spawn()
    {
        float randomX = Random.Range(minX, maxX);

        // Reset the velocity and position to avoid unwanted movement
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Reset the velocity to zero
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