using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerInputActions actions;
    public static event Action<Vector2> onMove;
    public static event Action onShoot;
    public static event Action onDash;
    public static event Action onMorph;
    public static event Action onPause;
    private void OnEnable()
    {
        actions = new PlayerInputActions();
        actions.Enable();

        actions.Player.Move.performed += OnMovePressed;
        actions.Player.Move.canceled += OnMovePressed;

        actions.Player.Shoot.performed += OnShootPressed;
        
        actions.Player.Dash.performed += OnDashPressed;
        
        actions.Player.Morph.performed += OnMorphPressed;

        actions.Player.Pause.performed += OnPausePressed;
    }
    
    private void OnDisable()
    {
        actions.Player.Move.performed -= OnMovePressed;
        actions.Player.Move.canceled -= OnMovePressed;
        
        actions.Player.Shoot.performed -= OnShootPressed;
        
        actions.Player.Dash.performed -= OnDashPressed;
        
        actions.Player.Morph.performed -= OnMorphPressed;
        
        actions.Player.Pause.performed -= OnPausePressed;
    }
    private void OnMovePressed(InputAction.CallbackContext obj)
    {
        onMove?.Invoke(obj.ReadValue<Vector2>());
    }
    private void OnShootPressed(InputAction.CallbackContext obj)
    {
        onShoot?.Invoke();
    }
    private void OnDashPressed(InputAction.CallbackContext obj)
    {
        onDash?.Invoke();
    }
    private void OnMorphPressed(InputAction.CallbackContext obj)
    {
        onMorph?.Invoke();
    }
    private void OnPausePressed(InputAction.CallbackContext obj)
    {
        onPause?.Invoke();
    }
}
