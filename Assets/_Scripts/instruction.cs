using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instruction : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private AudioClip clip;

    public void togglePanel()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(clip);
        if (panel.activeSelf)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
    }
}
