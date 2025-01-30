using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;
    public static Vector2 MousePosition; 

    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _mousePosition;

    public static InputAction OnShootPressed;
    public static InputAction OnDashPressed;
    public static InputAction OnPossessPressed;
    public static InputAction OnPausePressed;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _mousePosition = _playerInput.actions["MousePos"];

        OnDashPressed = _playerInput.actions["Dash"];
        OnShootPressed = _playerInput.actions["Shoot"];
        OnPossessPressed = _playerInput.actions["Possess"];
        OnPausePressed = _playerInput.actions["Pause"];
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();

        Vector2 screenMousePosition = _mousePosition.ReadValue<Vector2>();
        MousePosition = Camera.main.ScreenToWorldPoint(screenMousePosition);
    }
}
