using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Valumecontrol : MonoBehaviour
{
    public AudioMixer mixer;

    public void AudioControl_two()
    {
        mixer.SetFloat("SND", 0);
    }
    public void AudioControl_one()
    {
        mixer.SetFloat("SND", -15);
    }
    public void AudioControl_zero()
    {
        mixer.SetFloat("SND", -80);
    }
    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }


}
