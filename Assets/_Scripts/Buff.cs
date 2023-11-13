using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    int buffId = 0;

    [SerializeField]
    private AudioClip collect;

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision");
        if (other.gameObject.tag == "Player")
        {
            AudioSource aus = Camera.main.GetComponent<AudioSource>();
            aus.PlayOneShot(collect);
            Buffs.instance.currentBuff = Random.Range(1, Buffs.instance.buffCount);
            Destroy(gameObject);
        }
    }
}
