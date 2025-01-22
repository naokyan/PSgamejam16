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
    private bool _isMusicOff;
    private bool _isSoundOff;
    
    [Header("Player HUD")]
    public RectTransform BarsParnelRectTransform;
    public RectTransform WeaponImageRectTransform;
    
    [Header("Pause menu")]
    public RectTransform PauseRectTransform;
    
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
        PauseRectTransform.localScale = Vector3.zero;
        _isPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
            BarsParnelRectTransform.DOAnchorPosX(250f,.5f, true);
            WeaponImageRectTransform.DOAnchorPosX(-50f, .5f, true);
            PauseRectTransform.DOScale(Vector3.zero, .5f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                _isAnimating = false;
            });
        }
        else
        {
            _isPaused = true;
            BarsParnelRectTransform.DOAnchorPosX(-250f,.5f, true);
            WeaponImageRectTransform.DOAnchorPosX(250f, .5f, true);
            PauseRectTransform.DOScale(Vector3.one, .5f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                _isAnimating = false;
                Time.timeScale = 0f;
            });
        }
    }

    public void ExitToMainMenu()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void ToggleMusic()
    {
        _isMusicOff = !_isMusicOff;
        musicButton.GetComponent<Image>().sprite = _isMusicOff ? musicOff : musicOn;
        
        AudioController.Instance.ToggleMusic(_isMusicOff);
    }

    public void ToggleSound()
    {
        _isSoundOff = !_isSoundOff;
        soundButton.GetComponent<Image>().sprite = _isSoundOff ? soundOff : soundOn;
        
        AudioController.Instance.ToggleSound(_isSoundOff);
    }
}
