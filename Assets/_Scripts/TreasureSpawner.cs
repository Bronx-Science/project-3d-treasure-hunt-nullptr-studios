using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject treasurePrefab;

    [SerializeField]
    GameObject[] spawnedTreasures = new GameObject[26];

    int[] spawnedPrefabs;

    [SerializeField]
    int maxTreasures = 26;

    [SerializeField]
    int treasureCount = 0;

    [SerializeField]
    float spawnDelay = 15f;

    // public for testing purposes
    public float spawnTimer = 0f;

    [SerializeField]
    float bounds = 1000f;

    char[] letters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

    public static TreasureSpawner instance;

    void Start()
    {
        spawnedPrefabs = new int[letters.Length];
        instance = this;
        SpawnTreasure();
    }

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
        // get random letter
        int treasureIndex = Random.Range(0, letters.Length);
        if (letters[treasureIndex] == ' ')
        {
            SpawnTreasure();
            return;
        }
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
            spawnPosition = hit.point;
            GameObject treasure = Instantiate(treasurePrefab, spawnPosition, Quaternion.identity);

            spawnedTreasures[treasureIndex] = treasure;
            spawnedPrefabs[treasureIndex] = 1;
            treasure.GetComponent<Treasure>().letter = letters[treasureIndex];
            treasure.name = "Treasure " + letters[treasureIndex];
            letters[treasureIndex] = ' ';
            treasureCount++;
        }
        else
        {
            Debug.LogError("No ground found");
            SpawnTreasure();
            return;
        }
    }

    public GameObject[] getTreasures()
    {
        return spawnedTreasures;
    }

    public void clear()
    {
        for (int i = 0; i < spawnedTreasures.Length; i++)
        {
            if (spawnedTreasures[i] != null)
            {
                Destroy(spawnedTreasures[i]);
            }
        }
        treasureCount = 0;
        spawnedPrefabs = new int[letters.Length];
        letters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnDelay)
        {
            spawnTimer = 0f;
            if (treasureCount < maxTreasures)
            {
                SpawnTreasure();
            }
        }
    }
}
