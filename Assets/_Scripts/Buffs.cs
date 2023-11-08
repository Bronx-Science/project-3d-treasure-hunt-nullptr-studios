using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Buffs : MonoBehaviour
{
    public int currentBuff = 0;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] audioClips;

    [SerializeField]
    private TextMeshProUGUI buffText;

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
        buffText.text = "Current Buff: " + getBuffName();
    }

    public void useBuff()
    {
        Debug.Log("using buff");
        switch (currentBuff)
        {
            case 1:
                StartCoroutine(Sonar());
                break;
            case 2:
                GameManager.instance.addTime(20f);
                audioSource.PlayOneShot(audioClips[2], volume);
                break;
            case 3:
                StartCoroutine(Alacrity());
                break;
            case 4:
                StartCoroutine(Leaping());
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

    IEnumerator Alacrity()
    {
        float basespeed = playerMovement.instance.walkSpeed;
        float baseSprint = playerMovement.instance.sprintSpeed;

        playerMovement.instance.walkSpeed = basespeed * 2;
        playerMovement.instance.sprintSpeed = baseSprint * 2;

        audioSource.PlayOneShot(audioClips[3], volume);

        yield return new WaitForSeconds(10);

        playerMovement.instance.walkSpeed = 10f;
        playerMovement.instance.sprintSpeed = 15f;
    }

    IEnumerator Leaping()
    {
        float basejump = playerMovement.instance.jumpPower;

        playerMovement.instance.jumpPower = basejump * 2;

        audioSource.PlayOneShot(audioClips[4], volume);

        yield return new WaitForSeconds(10);

        playerMovement.instance.jumpPower = basejump;
    }

    public string getBuffName()
    {
        switch (currentBuff)
        {
            case 1:
                return "Sonar";
            case 2:
                return "Add Time";
            case 3:
                return "Alacrity";
            case 4:
                return "Leaping";

            default:
                return "None";
        }
    }
}
