using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MuteButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    AudioMixer BGMmixer;
    [SerializeField]
    AudioMixer SFXmixer;
    [SerializeField]
    AudioClip clip;
    AudioSource playtest;

    public void OnBGMMuted(bool value)
    {
        if (!value)
            BGMmixer.SetFloat("Volume", -80);
        else
            BGMmixer.SetFloat("Volume", 0);
    }

    public void OnSFXMuted(bool value)
    {
        if (!value)
            SFXmixer.SetFloat("Volume", -80);
        else
            SFXmixer.SetFloat("Volume", 0);
        playtest = Camera.main.GetComponent<AudioSource>();
        playtest.PlayOneShot(clip);
    }
}
