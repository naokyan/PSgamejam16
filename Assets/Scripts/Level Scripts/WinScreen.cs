using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public GameObject winScreenUI;
    public string mainMenuSceneName = "MainMenuScene";

    private void Start()
    {
        if (winScreenUI != null)
            winScreenUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(ShowWinScreen());
    }

    private IEnumerator ShowWinScreen()
    {
        if (winScreenUI != null)
        {
            winScreenUI.SetActive(true);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("MainMenuScene");
        }
    }

}
