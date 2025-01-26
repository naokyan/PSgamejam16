using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool CanPossess;
    public static bool IsPossessing;

    private static GameObject _originalPlayer;
    private static GameObject _currentPlayer;

    [SerializeField] private Slider _playerHPBar;
    [SerializeField] private Image _playerHPBarColor;

    public static int _playerHP;

    private void Start()
    {
        CanPossess = true;
        IsPossessing = false;
        _originalPlayer = GameObject.FindGameObjectWithTag("Player");
        _playerHP = 8;//player's default HP is set to 8
    }


    private void Update()
    {
        _currentPlayer = GameObject.FindGameObjectWithTag("Player");
        if (_originalPlayer != _currentPlayer)
        {
            _originalPlayer.transform.position = _currentPlayer.transform.position;
        }

        //change player's HP bar here
        _playerHPBar.value = _playerHP / 8f;

        if (_playerHP < 3)
        {
            _playerHPBarColor.color = Color.red;
        }
        else if (_playerHP > 5)
        {
            _playerHPBarColor.color = Color.green;
        }
        else
        {
            _playerHPBarColor.color = Color.yellow;
        }

        if (_playerHP == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public static void PossessEnemy(GameObject enemy)
    {
        IsPossessing = true;

        _currentPlayer.SetActive(false);

        enemy.GetComponent<PlayerMovement>().enabled = true;
        enemy.GetComponent<Shooting>().enabled = true;
        enemy.GetComponent<Possessing>().enabled = true;

        enemy.tag = "Player";
    }

    public static void BackToOriginalForm()
    {
        IsPossessing = false;

        _currentPlayer.SetActive(false);
        _originalPlayer.SetActive(true);
    }

    public static void ChangePlayerHP(int hitpoints)
    {
        _playerHP = _playerHP + hitpoints;
    }
}
