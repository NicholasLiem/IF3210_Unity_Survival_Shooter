using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadAudioSettings();
    }

    private void LoadAudioSettings()
    {
        if (PlayerPrefs.HasKey("masterVol"))
        {
            mixer.SetFloat("masterVol", PlayerPrefs.GetFloat("masterVol", 0));
        }
        else
        {
            mixer.SetFloat("masterVol", 0);
        }
    }
}
