using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool CanPossess;
    public static bool IsPossessing;

    private static GameObject _originalPlayer;
    private static GameObject _currentPlayer;

    private void Start()
    {
        CanPossess = true;
        IsPossessing = false;
        _originalPlayer = GameObject.FindGameObjectWithTag("Player");
    }


    private void Update()
    {
        _currentPlayer = GameObject.FindGameObjectWithTag("Player");
        if (_originalPlayer != _currentPlayer)
        {
            _originalPlayer.transform.position = _currentPlayer.transform.position;
        }
    }

    public static void PossessEnemy(GameObject enemy)
    {
        IsPossessing = true;

        _currentPlayer.SetActive(false);

        enemy.GetComponent<PlayerMovement>().enabled = true;
        enemy.GetComponent<Shooting>().enabled = true;
        enemy.GetComponentInChildren<GunController>().enabled = true;
        enemy.GetComponentInChildren<Possessing>().enabled = true;

        enemy.tag = "Player";
    }

    public static void BackToOriginalForm()
    {
        IsPossessing = false;

        _currentPlayer.SetActive(false);
        _originalPlayer.SetActive(true);
    }
}
