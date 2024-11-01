using System.Collections;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject objectPrefab1;    // First prefab to spawn
    public GameObject objectPrefab2;    // Second prefab to spawn
    public float minX = -1.5f;            // Minimum X position
    public float maxX = 1.5f;             // Maximum X position
    public float fixedY = 4f;           // Fixed Y position for spawning
    public int objectCount = 10;        // Total number of objects to spawn
    public float spawnInterval = 1f;    // Time delay between spawns (in seconds)

    private GameObject[] spawnedObjects; // Array to store references to spawned objects

    void Start()
    {
        spawnedObjects = new GameObject[objectCount];
        StartCoroutine(SpawnObjectsWithDelay());
    }

    IEnumerator SpawnObjectsWithDelay()
    {
        for (int i = 0; i < objectCount; i++)
        {
            // Generate a random X position within the specified range
            float randomX = Random.Range(minX, maxX);

            // Randomly select one of the two prefabs
            GameObject selectedPrefab = (Random.value > 0.5f) ? objectPrefab1 : objectPrefab2;

            // Spawn the randomly selected object at (randomX, fixedY, 0)
            GameObject newObj = Instantiate(selectedPrefab, new Vector3(randomX, fixedY, 0), Quaternion.identity);

            // Store the spawned object in the array
            spawnedObjects[i] = newObj;

            // Wait for the specified interval before spawning the next object
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
