using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSliderManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    AudioMixer BGMmixer;
    [SerializeField]
    AudioMixer SFXmixer;

    public void OnBGMChanged(float value)
    {
        BGMmixer.SetFloat("Volume", value - 80);
    }

    public void OnSFXChanged(float value)
    {
        SFXmixer.SetFloat("Volume", value - 80);
    }
}
