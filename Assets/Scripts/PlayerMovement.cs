using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private float _speed = 5f;
    
    public float dashDistance = 2f;
    public float dashDuration = 0.5f;
    private bool canDash = true;

    private Vector2 _moveDir;
    private Vector2 _movement;
    private Vector2 _mousePos;
    
    private Rigidbody2D _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        InputManager.onMove += OnMovePlayer;
        InputManager.onDash += Dash;
    }
    private void OnDisable()
    {
        InputManager.onMove -= OnMovePlayer;
        InputManager.onDash -= Dash;
    }
    void Update()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }
    private void FixedUpdate()
    {
        _rb.velocity = _movement * _speed;
        
        Vector2 dir = _mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    private void OnMovePlayer(Vector2 obj)
    {
        _movement = obj;
    }
    
    private void Dash()
    {
        if (canDash)
        {
            canDash = false;
            _moveDir = _movement;
            Vector2 dashTarget = (Vector2)transform.position + _moveDir * dashDistance;

            _rb.DOMove(dashTarget, dashDuration).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                canDash = true;
            });
        }
    }

}
