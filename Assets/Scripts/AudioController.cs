using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    public AudioMixer mixer;
    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ToggleMusic(bool muted)
    {
        mixer.SetFloat("Music",muted ? -80f : 0f);
    }

    public void ToggleSound(bool muted)
    {
        mixer.SetFloat("SFX",muted ? -80f : 0f);
    }
}
