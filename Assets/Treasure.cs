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

    // Start is called before the first frame update


    void Awake()
    {
        beacon.SetActive(false);
        wordManager = GameObject.Find("word").GetComponent<Words>();
    }

    // Update is called once per frame
    void Update() { }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            wordManager.revealLetter(letter);
            Destroy(gameObject);
        }
    }
}
