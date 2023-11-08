using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField]
    private GameObject beacon;
    public char letter;

    [SerializeField]
    private Words wordManager;

    [SerializeField] private AudioClip collect;

    // Start is called before the first frame update


    void Awake()
    {
        beacon.SetActive(false);
        wordManager = GameObject.Find("word").GetComponent<Words>();
    }

    public void enableBeacon()
    {
        beacon.SetActive(true);
    }

    public void disableBeacon()
    {
        beacon.SetActive(false);
    }

    // Update is called once per frame
    void Update() { }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            AudioSource aus = Camera.main.GetComponent<AudioSource>();
            aus.PlayOneShot(collect);
            wordManager.revealLetter(letter);
            Destroy(gameObject);
        }
    }
}
