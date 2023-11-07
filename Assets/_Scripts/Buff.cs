using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    int buffId = 0;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Buffs.instance.currentBuff = Random.Range(1, Buffs.instance.buffCount);
            Destroy(gameObject);
        }
    }
}
