using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private Vector2 _movement;

    private Rigidbody2D _rb;
    private Animator _animator;

    private const string _horizontal = "Horizontal"; 
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";
    private const string _lastVertical = "LastVertical";

    private bool _canDash = true;
    private bool _isDashing;

    [SerializeField] private float _dashingPower = 24f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashingCooldown = 1f;
    [SerializeField] private TrailRenderer _tr;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isDashing)
        {
            return;
        }

        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);

        _rb.velocity = _movement * _moveSpeed;

        _animator.SetFloat(_horizontal, _movement.x);
        _animator.SetFloat(_vertical, _movement.y);

        if (_movement != Vector2.zero)
        {
            _animator.SetFloat(_lastHorizontal, _movement.x);
            _animator.SetFloat(_lastVertical, _movement.y);
        }

        if (InputManager.OnDashPressed.WasPressedThisFrame() && _canDash) 
        {
            StartCoroutine(Dash());
        }

    }

    private void FixedUpdate()
    {
        if (_isDashing)
        {
            return;
        }
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;

        // Determine the dash direction
        Vector2 dashDirection = _movement.normalized;

        // If no movement input, use the last faced direction
        if (dashDirection == Vector2.zero)
        {
            dashDirection = new Vector2(_animator.GetFloat(_lastHorizontal), _animator.GetFloat(_lastVertical)).normalized;
        }

        // Apply the dash velocity
        _rb.velocity = dashDirection * _dashingPower;
        _tr.emitting = true;

        yield return new WaitForSeconds(_dashingTime);

        _tr.emitting = false;
        _isDashing = false;

        // Cooldown before dashing again
        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }

}
