using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
