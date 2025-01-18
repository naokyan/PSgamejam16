using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public Button musicButton;
    public Button soundButton;
    
    [Header("Music")]
    public Sprite musicOff;
    public Sprite musicOn;
    
    [Header("Sound")]
    public Sprite soundOff;
    public Sprite soundOn;
    
    public void PauseGame()
    {
        
    }

    public void ExitToMainMenu()
    {
        //TODO SET MAIN MENU SCENE
        //SceneManager.LoadScene(0);
    }

    public void ToggleMusic()
    {
        if (true)
        {
            musicButton.GetComponent<Image>().sprite = musicOff;
        }
        else
        {
            
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
            
        }
    }
}
