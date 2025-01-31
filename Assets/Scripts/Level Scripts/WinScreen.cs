using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public GameObject winScreenUI;
    public string mainMenuSceneName = "MainMenuScene";
    public Button mainMenuButton;

    private void Start()
    {
        if (winScreenUI != null)
            winScreenUI.SetActive(false);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            ShowWinScreen();
    }

    private void ShowWinScreen()
    {
        if (winScreenUI != null)
        {
            winScreenUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
