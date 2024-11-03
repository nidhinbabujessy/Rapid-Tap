using System.Collections;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab1;
    [SerializeField] private GameObject objectPrefab2;
    [SerializeField] private float minX = -1.5f;
    [SerializeField] private float maxX = 1.5f;
    [SerializeField] private float fixedY = 4f;
    [SerializeField] private int objectCount = 10;
    [SerializeField] private float spawnInterval = 1f;



    private GameObject[] spawnedObjects; 

    void Start()
    {
      
        spawnedObjects = new GameObject[objectCount];
        StartCoroutine(SpawnObjectsWithDelay());
    }

    IEnumerator SpawnObjectsWithDelay()
    {
            for (int i = 0; i < objectCount; i++)
            { 
                float randomX = Random.Range(minX, maxX);
                GameObject selectedPrefab = (Random.value > 0.5f) ? objectPrefab1 : objectPrefab2;
                GameObject newObj = Instantiate(selectedPrefab, new Vector3(randomX, fixedY, 0), Quaternion.identity);
                spawnedObjects[i] = newObj;
                yield return new WaitForSeconds(spawnInterval);
            }
        
    }
    public void restartLoop()
    {
        StartCoroutine(SpawnObjectsWithDelay());
    }
    
}