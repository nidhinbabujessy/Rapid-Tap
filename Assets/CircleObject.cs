using UnityEngine;

public class CircleObject : MonoBehaviour
{
    public float minX = -1.5f;         // Minimum X position for repositioning
    public float maxX = 1.5f;          // Maximum X position for repositioning
    public float fixedY = 4f;           // Fixed Y position for repositioning

    private Rigidbody2D rb;  // Reference to the Rigidbody2D component


    public bool enemy;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    private void OnBecameInvisible()
    {
        Spawn();
    }

    private void OnMouseDown()
    {
        Spawn();
    }

    void Spawn()
    {
        // Generate a random X position within the specified range
        float randomX = Random.Range(minX, maxX);

        // Reset the velocity to stop increasing speed
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Reset the velocity to zero
        }

        // Set the object's position to the new random X and fixed Y
        transform.position = new Vector3(randomX, fixedY, 0);
    }

    void score()
    {
        if (enemy)
        {
        }
        else if (!enemy)
        {
        }
    }
}
