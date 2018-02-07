using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuIngame : MonoBehaviour {

    public AudioMixer audioMixer;

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicvolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxvolume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
