using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffs : MonoBehaviour
{
    public int currentBuff = 0;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] audioClips;

    // buffs by id
    // 0 - none
    // 1 - sonar
    // 2 - add time
    public int buffCount = 1;

    public static Buffs instance;

    float volume = 0.4f;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            useBuff();
        }
    }

    public void useBuff()
    {
        Debug.Log("using buff");
        switch (currentBuff)
        {
            case 1:
                StartCoroutine(Sonar());
                break;
            default:
                break;
        }
        currentBuff = 0;
    }

    IEnumerator Sonar()
    {
        // get all treasures
        GameObject[] treasures = TreasureSpawner.instance.getTreasures();
        Debug.Log(treasures);

        // enable beacon on all treasures
        foreach (GameObject treasure in treasures)
        {
            if (treasure == null)
            {
                continue;
            }
            treasure.GetComponent<Treasure>().enableBeacon();
        }
        // play sound
        audioSource.PlayOneShot(audioClips[1], volume);
        // wait 10 seconds
        yield return new WaitForSeconds(10);
        // disable beacon on all treasures
        foreach (GameObject treasure in treasures)
        {
            if (treasure == null)
            {
                continue;
            }
            treasure.GetComponent<Treasure>().disableBeacon();
        }
    }
}
