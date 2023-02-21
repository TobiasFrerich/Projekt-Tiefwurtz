using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class timer : MonoBehaviour
{
    public AudioMixer AudMix;
    const string MIXER_MASTER = "MasterVolume";
    public void SetVolume(float volume)
    {
        AudMix.SetFloat("MasterVolume", volume);
    }
}
