using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _smoothTime = 0.05f; 
    [SerializeField] private Vector2 _cameraBounds = new Vector2(5f, 3f); 
    private Vector3 _offset = new Vector3(0f, 0f, -10f); 
    private Vector3 _velocity = Vector3.zero;
    private Camera _mainCamera;
    private Transform _target;

    void Start()
    {
        _mainCamera = Camera.main;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _target = player.transform;
        }
    }

    void Update()
    {
        if (_target == null) return;

        Vector2 mousePosition = InputManager.MousePosition;
        Vector3 mouseWorldPosition = new Vector3(mousePosition.x, mousePosition.y, 0f);

        Vector3 mouseOffset = mouseWorldPosition - _target.position;

        mouseOffset.x = Mathf.Clamp(mouseOffset.x, -_cameraBounds.x, _cameraBounds.x);
        mouseOffset.y = Mathf.Clamp(mouseOffset.y, -_cameraBounds.y, _cameraBounds.y);

        Vector3 targetPosition = _target.position + mouseOffset + _offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
    }
}
