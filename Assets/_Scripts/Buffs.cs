using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffs : MonoBehaviour
{
    public int currentBuff = 0;

    // buffs by id
    // 0 - none
    // 1 - sonar

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
        // enable beacon on all treasures
        foreach (GameObject treasure in treasures)
        {
            treasure.GetComponent<Treasure>().enableBeacon();
        }
        // wait 10 seconds
        yield return new WaitForSeconds(10);
        // disable beacon on all treasures
        foreach (GameObject treasure in treasures)
        {
            treasure.GetComponent<Treasure>().disableBeacon();
        }
    }
}
