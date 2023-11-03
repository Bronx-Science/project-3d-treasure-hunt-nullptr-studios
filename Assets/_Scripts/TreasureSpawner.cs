using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] treasurePrefabs = new GameObject[3];
    [SerializeField] GameObject[] spawnedTreasures = new GameObject[3];
    
    int[] spawnedPrefabs;
    [SerializeField] int maxTreasures = 5;
    [SerializeField] int treasureCount = 0;
    [SerializeField] float spawnDelay = 15f;
    // public for testing purposes
    public float spawnTimer = 0f;
    [SerializeField] float bounds = 1000f;

    public void SpawnTreasure()
    {
        if (treasureCount >= maxTreasures)
        {
            return;
        }
        // go to random position
        float x = Random.Range(0, bounds);
        float z = Random.Range(0, bounds);
        transform.position = new Vector3(x, 100f, z);
        // pick a random treasure prefab
        int treasureIndex = Random.Range(0, treasurePrefabs.Length);
        if (spawnedPrefabs[treasureIndex] == 1)
        {
            SpawnTreasure();
            return;
        }
        // raycast down to get treasure spawn position
        RaycastHit hit;
        Vector3 spawnPosition;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100f))
        {
           spawnPosition  = hit.point;
           GameObject treasure = Instantiate(treasurePrefabs[treasureIndex], spawnPosition, Quaternion.identity);
            spawnedTreasures[treasureIndex] = treasure;
            spawnedPrefabs[treasureIndex] = 1;
            treasureCount++;
        }
        else
        {
            Debug.LogError("No ground found");
            SpawnTreasure();
            return;
        }
        
        
    }

    // Start is called before the first frame update
    void Start()
    {
       spawnedPrefabs = new int[treasurePrefabs.Length]; 
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnDelay){
            spawnTimer = 0f;
            if (treasureCount < maxTreasures)
            {
                SpawnTreasure();
            }
        }
    }
}
