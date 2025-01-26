using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;

    private const string _aimHorizontal = "AimHorizontal";
    private const string _aimVertical = "AimVertical";
    private const string _aimLastHorizontal = "AimLastHorizontal";
    private const string _aimLastVertical = "AimLastVertical";

    private const string _moveHorizontal = "MoveHorizontal";
    private const string _moveVertical = "MoveVertical";

    private bool _canDash = true;
    private bool _isDashing;

    [SerializeField] private float _dashingPower = 24f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashingCooldown = 1f;
    [SerializeField] private TrailRenderer _tr;

    private Camera _mainCamera;

    private SpriteRenderer _playerSprite;
    private bool _isInvincible = false;
    [SerializeField] private float _blinkDuration = 2f;//this determines how long player's sprite blinks when running into an enemy
    [SerializeField] private float _blinkInterval = 0.2f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _mainCamera = Camera.main;
        _playerSprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_isDashing) return;

        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);

        _animator.SetFloat(_moveHorizontal, _movement.x);
        _animator.SetFloat(_moveVertical, _movement.y);

        _rb.velocity = _movement * _moveSpeed;

        Vector2 mousePosition = InputManager.MousePosition;
        Vector2 aimDirection = (mousePosition - _rb.position).normalized;

        _animator.SetFloat(_aimHorizontal, aimDirection.x);
        _animator.SetFloat(_aimVertical, aimDirection.y);

        
        if (aimDirection != Vector2.zero)
        {
            _animator.SetFloat(_aimLastHorizontal, aimDirection.x);
            _animator.SetFloat(_aimLastVertical, aimDirection.y);
        }

        _canDash = false;//players can only dash if there is movement inputs
        if (_movement != Vector2.zero)
        {
            _canDash = true;
        }

        if (InputManager.OnDashPressed.WasPressedThisFrame() && _canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;

        Vector2 dashDirection = _rb.velocity.normalized;

        if (dashDirection == Vector2.zero)
        {
            dashDirection = new Vector2(_animator.GetFloat(_aimLastHorizontal), _animator.GetFloat(_aimLastVertical)).normalized;
        }

        _rb.velocity = dashDirection * _dashingPower;
        _tr.emitting = true;

        yield return new WaitForSeconds(_dashingTime);

        _tr.emitting = false;
        _isDashing = false;

        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && !_isInvincible)
        {
            GameManager.ChangePlayerHP(-1);
            StartCoroutine(BlinkAndInvincibility());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && !_isInvincible)
        {
            GameManager.ChangePlayerHP(-1);
        }
    }

    private IEnumerator BlinkAndInvincibility()
    {
        _isInvincible = true;
        float elapsedTime = 0f;

        while (elapsedTime < _blinkDuration)
        {
            _playerSprite.enabled = !_playerSprite.enabled; 
            elapsedTime += _blinkInterval;
            yield return new WaitForSeconds(_blinkInterval);
        }

        _playerSprite.enabled = true; 
        _isInvincible = false;
    }
}

