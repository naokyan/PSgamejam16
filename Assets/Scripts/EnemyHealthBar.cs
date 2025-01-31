using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Vector3 _offset = new Vector3(0, 1.5f, 0);
    private Image _fill;
    private Canvas _canvas;
    public GameObject _canvasObject;

    private float _maxHealth = 8f;
    public int _currentHealth = 8;

    public bool CanBePossessed;

    [SerializeField] private int _possessUnderHP;

    private void Start()
    {
        CreateHealthBar();
        CanBePossessed = false;
    }

    private void CreateHealthBar()
    {
        _currentHealth = (int)_maxHealth;

        _canvasObject = new GameObject("HealthBarCanvas");
        _canvas = _canvasObject.AddComponent<Canvas>();
        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        GameObject borderObject = new GameObject("Border");
        borderObject.transform.SetParent(_canvas.transform);
        Image border = borderObject.AddComponent<Image>();
        border.rectTransform.sizeDelta = new Vector2(100, 20);
        border.color = Color.black;

        GameObject fillObject = new GameObject("Fill");
        fillObject.transform.SetParent(borderObject.transform);
        _fill = fillObject.AddComponent<Image>();
        _fill.rectTransform.sizeDelta = new Vector2(98, 18);
        _fill.rectTransform.anchorMin = new Vector2(0, 0.5f); // Anchor to the left center
        _fill.rectTransform.anchorMax = new Vector2(0, 0.5f); // Anchor to the left center
        _fill.rectTransform.pivot = new Vector2(0, 0.5f); // Pivot at the left edge
        _fill.rectTransform.anchoredPosition = Vector2.zero;
        _fill.color = Color.green;

        UpdateHealthBarPosition();
    }

    private void UpdateHealthBarPosition()
    {
        if (_fill != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + _offset);
            _fill.transform.parent.position = screenPosition;
        }
    }

    public void UpdateHealth(int newHealth)
    {
        _currentHealth = Mathf.Clamp(newHealth, 0, (int)_maxHealth);
        float fillAmount = (float)_currentHealth / _maxHealth;

        _fill.rectTransform.sizeDelta = new Vector2(98 * fillAmount, 18);

        if (fillAmount > 0.5f)
        {
            _fill.color = Color.Lerp(Color.yellow, Color.green, (fillAmount - 0.5f) * 2f);
        }
        else
        {
            _fill.color = Color.Lerp(Color.red, Color.yellow, fillAmount * 2f);
        }
    }

    private void LateUpdate()
    {
        UpdateHealthBarPosition();
    }

    private void Update()
    {
        if (_currentHealth < _possessUnderHP)
        {
            CanBePossessed = true;
        }

        if (_currentHealth <= 0)
        {
            gameObject.SetActive(false);
            _canvasObject.SetActive(false);
        }

        /*
        if (GameManager.NewGame)
        {
            _currentHealth = (int)_maxHealth;
            UpdateHealth(_currentHealth);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!this.enabled)
        {
            return;
        }

        if (other.CompareTag("PlayerBullet"))
        {
            _currentHealth--;
            UpdateHealth(_currentHealth);
        }
    }
}