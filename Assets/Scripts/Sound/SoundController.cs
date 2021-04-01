using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public string volumeParameter;
    public AudioMixer audioMixer;
    private UnityEvent mute;
    private Slider musicSlider;
    private Slider sfxSlider;

    void Awake()
    {
        GetVolume(audioMixer, volumeParameter);
        Debug.Log(musicSlider.value);
        Debug.Log(musicSlider.value);
    }

    void Start()
    {
        if (mute == null)
        {
            mute = new UnityEvent();
            mute.AddListener(Muting);
        }
    }
    private void Update()
    {
        musicSlider = GameObject.FindGameObjectWithTag("MusicOnOffButton").GetComponent<Slider>();
        sfxSlider = GameObject.FindGameObjectWithTag("SfxOnOffSlider").GetComponent<Slider>();
        GetVolume(audioMixer, volumeParameter);
    }

    void Muting()
    {
        if (musicSlider.value == 0 || sfxSlider.value == 0)
        {
            SetVolume(audioMixer, volumeParameter, 0.0f);
            mute.Invoke();
        }
        else
        {
            SetVolume(audioMixer, volumeParameter, -80.0f);
        }
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
