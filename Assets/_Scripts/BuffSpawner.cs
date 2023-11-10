using System.Collections;
using UnityEngine;

public class BuffSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject treasurePrefab;

    [SerializeField]
    ArrayList spawnedTreasures = new ArrayList();

    [SerializeField]
    int maxTreasures = 10;

    [SerializeField]
    int treasureCount = 0;

    [SerializeField]
    float spawnDelay = 15f;

    // public for testing purposes
    public float spawnTimer = 0f;

    [SerializeField]
    float bounds = 1000f;

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

        // raycast down to get treasure spawn positio
        RaycastHit hit;
        Vector3 spawnPosition;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100f))
        {
            spawnPosition = hit.point;
            GameObject treasure = Instantiate(treasurePrefab, spawnPosition, Quaternion.identity);
            spawnedTreasures.Add(treasure);
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
    void Start() { 
        SpawnTreasure();
    }

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
