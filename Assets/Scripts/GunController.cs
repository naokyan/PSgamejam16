using System.Diagnostics;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private Rigidbody2D _rb;

    private Transform _player;
    private Transform _rightHand;
    private Transform _leftHand;
    private Transform _currentHand;

    private SpriteRenderer _currentSpriteRenderer;
    private SpriteRenderer _gunSpriteRenderer;

    [SerializeField] private float gunSize = 3f; 

    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody2D>();

        _player = GameObject.FindWithTag("Player").transform;
        _rightHand = transform.parent.Find("Right Hand");
        _leftHand = transform.parent.Find("Left Hand");

        _currentSpriteRenderer = GetComponentInParent<SpriteRenderer>();
        _gunSpriteRenderer = GetComponent<SpriteRenderer>();

        ApplyGunSize(); 
    }

    private void Update()
    {
        if (GetComponentInParent<PlayerMovement>().enabled)
        {
            PlayerInControl();
        }
        else
        {
            EnemyInControl();
        }
    }

    private void PlayerInControl()
    {
        Vector2 mousePosition = InputManager.MousePosition;
        Vector2 aimDirection = (mousePosition - _rb.position).normalized;

        _currentHand = (mousePosition.x > _player.position.x) ? _rightHand : _leftHand;
        transform.position = _currentHand.position;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        _gunSpriteRenderer.flipX = (_currentHand == _leftHand);

        _gunSpriteRenderer.sortingOrder = (mousePosition.y > _player.position.y) ?
            _currentSpriteRenderer.sortingOrder - 1 :
            _currentSpriteRenderer.sortingOrder + 1;
    }

    private void EnemyInControl()
    {
        Vector2 aimDirection = _rb.velocity.normalized;
        if (aimDirection == Vector2.zero)
        {
            aimDirection = ((Vector2)_player.position - _rb.position).normalized;
        }

        _currentHand = (aimDirection.x > 0) ? _rightHand : _leftHand;
        transform.position = _currentHand.position;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        _gunSpriteRenderer.flipX = (_currentHand == _leftHand);

        _gunSpriteRenderer.sortingOrder = (_player.position.y > transform.position.y) ?
            _currentSpriteRenderer.sortingOrder - 1 :
            _currentSpriteRenderer.sortingOrder + 1;
    }

    private void ApplyGunSize()
    {
        transform.localScale = Vector3.one * gunSize;
    }

    public void SetGunSize(float newSize)
    {
        gunSize = newSize;
        ApplyGunSize();
    }
}
