using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public RectTransform GameOverPanel;
    
    private void Start()
    {
        GameOverPanel.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (Player.Instance)
        {
            if (Player.Instance.IsDead())
            {
                GameOverPanel.DOScale(Vector3.one, .5f).SetEase(Ease.OutBack);
            }
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
