using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Specialized;
using System.Diagnostics;

public class GameManager : MonoBehaviour
{
    public static bool IsPossessing;

    private static GameObject _originalPlayer;
    private static GameObject _currentPlayer;

    [SerializeField] private Slider _playerHPBar;
    [SerializeField] private Image _playerHPBarColor;
    [SerializeField] private GameObject _deathScreen;

    private float _playerMaxHP;
    public static int _playerHP;

    //public static bool NewGame;

    private bool _isPaused;

    [SerializeField] private GameObject pauseMenuUI;

    private void Start()
    {
        IsPossessing = false;
        _originalPlayer = GameObject.FindGameObjectWithTag("Player");

        _playerMaxHP = 8f; // Player's default HP is set to 8
        _playerHP = (int)_playerMaxHP;

        _deathScreen.SetActive(false);

        //NewGame = false;

        _isPaused = false;
    }

    private void Update()
    {
        _currentPlayer = GameObject.FindGameObjectWithTag("Player");
        if (_originalPlayer != _currentPlayer)
        {
            _originalPlayer.transform.position = _currentPlayer.transform.position;
        }

        _playerHPBar.value = _playerHP / _playerMaxHP;

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

        if (_playerHP <= 0)
        {
            StartCoroutine(HandlePlayerDeath());
        }

        if (InputManager.OnPausePressed.WasPressedThisFrame())
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;
        pauseMenuUI.SetActive(_isPaused);
        _currentPlayer.GetComponent<PlayerMovement>().enabled = !_isPaused;

        Time.timeScale = _isPaused ? 0 : 1;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    private IEnumerator HandlePlayerDeath()
    {
        /*
        _deathScreen.SetActive(true);
        NewGame = true;

        _originalPlayer.transform.position = SpawnPoint;
        if (_currentPlayer != _originalPlayer)
        {
            _currentPlayer.SetActive(false);
            _originalPlayer.SetActive(true);
            _currentPlayer = _originalPlayer;
        }
        _playerHP = (int)_playerMaxHP;

        yield return new WaitForSeconds(2);
        NewGame = false;
        _deathScreen.SetActive(false);*/

        _deathScreen.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void PossessEnemy(GameObject enemy)
    {
        IsPossessing = true;

        _currentPlayer.SetActive(false);

        enemy.GetComponent<PlayerMovement>().enabled = true;
        enemy.GetComponent<Shooting>().enabled = true;
        enemy.GetComponent<Possessing>().enabled = true;

        enemy.GetComponent<EnemyController>().enabled = false;

        enemy.GetComponent<EnemyHealthBar>()._canvasObject.SetActive(false);
        enemy.GetComponent<EnemyHealthBar>().enabled = false;

        enemy.tag = "Player";
        enemy.layer = LayerMask.NameToLayer("Player");
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
