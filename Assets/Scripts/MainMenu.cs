using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "morphoid.mp4");
        videoPlayer.Play();
    }

    private void Update()
    {
        if (Input.anyKey)
            StartGame();
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
