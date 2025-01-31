using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundMusic; 
    private AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); 
        audioSource.clip = backgroundMusic;
        audioSource.loop = true; 
        audioSource.playOnAwake = true;
        audioSource.volume = 0.5f;
        audioSource.Play(); 
    }

    public void ToggleMusic()
    {
        if (audioSource.isPlaying)
            audioSource.Pause();
        else
            audioSource.Play();
    }
}
