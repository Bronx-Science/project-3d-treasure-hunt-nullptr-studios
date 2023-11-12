using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bushspawner : MonoBehaviour
{
    //BROKEN??
    [SerializeField]
    int numBushes = 100;

    [SerializeField]
    private GameObject empty;

    [SerializeField]
    GameObject[] bushPrefabs;

    [SerializeField]
    float bounds = -100f;

    [SerializeField]
    private GameObject[] bushes;

    // Start is called before the first frame update
    void Start()
    {
        bushes = new GameObject[numBushes];
        for (int i = 0; i < numBushes; i++)
        {
            float x = Random.Range(-bounds, bounds);
            float z = Random.Range(-bounds, bounds);
            transform.position = new Vector3(x, 500f, z);

            RaycastHit hit;
            Vector3 spawnPosition;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 500f))
            {
                spawnPosition = hit.point;
                int bushIndex = Random.Range(0, bushPrefabs.Length);
                bushes[i] = Instantiate(bushPrefabs[bushIndex], spawnPosition, Quaternion.identity);
                bushes[i].transform.SetParent(empty.transform);
            }
            else
            {
                Debug.LogError("No ground found");
                continue;
            }
        }
    }

    // Update is called once per frame
}
