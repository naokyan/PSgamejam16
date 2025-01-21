using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Movement Attributes")]
    [SerializeField] private Transform target;

    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.05f;
    private Vector3 velocity = Vector3.zero;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
