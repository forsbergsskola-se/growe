using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    public string volumeParameter;
    public AudioMixer audioMixer;
    
    void Awake()
    {
        GetVolume(audioMixer, volumeParameter);
    }

    private void Update()
    {
        //if sfx slider is off
        //SetVolume(audioMixer, volumeParameter, 0.0f);
        //if music slider is off
        //SetVolume(audioMixer, volumeParameter, 0.0f);
    }
    
    public static void SetVolume(AudioMixer mixer, string exposedName, float value)
    {
        mixer.SetFloat(exposedName, Mathf.Lerp(-80.0f, 0.0f, Mathf.Clamp01(value)));
    }
    
    public static float GetVolume(AudioMixer mixer, string exposedName)
    {
        if (mixer.GetFloat(exposedName, out float volume))
        {
            return Mathf.InverseLerp(-80.0f, 0.0f, volume);
        }
 
        return 0f;
    }
}
