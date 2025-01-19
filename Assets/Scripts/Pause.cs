using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private bool _isPaused;
    private bool _isAnimating;
    
    public GameObject PauseUI;
    public RectTransform rectTransform;
    
    [Header("Buttons")]
    public Button musicButton;
    public Button soundButton;
    
    [Header("Music")]
    public Sprite musicOff;
    public Sprite musicOn;
    
    [Header("Sound")]
    public Sprite soundOff;
    public Sprite soundOn;

    private void Start()
    {
        rectTransform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (_isAnimating) return;
        _isAnimating = true;
        if (_isPaused)
        {
            _isPaused = false;
            Time.timeScale = 1f;
            rectTransform.DOScale(Vector3.zero, .5f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                _isAnimating = false;
            });
        }
        else
        {
            _isPaused = true;
            rectTransform.DOScale(Vector3.one, .5f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                _isAnimating = false;
                Time.timeScale = 0f;
            });
        }
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToggleMusic()
    {
        if (true)
        {
            musicButton.GetComponent<Image>().sprite = musicOff;
        }
        else
        {
            //soundButton.GetComponent<Image>().sprite = musicOn;
        }
    }

    public void ToggleSound()
    {
        if (true)
        {
            soundButton.GetComponent<Image>().sprite = soundOff;
        }
        else
        {
            //soundButton.GetComponent<Image>().sprite = soundOn;
        }
    }
}
