using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;

    private PlayerInput _playerInput;
    private InputAction _moveAction;

    public static InputAction OnShootPressed;
    public static InputAction OnDashPressed;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        OnDashPressed = _playerInput.actions["Dash"];
        OnShootPressed = _playerInput.actions["Shoot"];
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
    }
}
