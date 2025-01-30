using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 24f;
    [SerializeField] private Rigidbody2D _rb;

    private Vector2 _facingDirection;

    public void Initialize(Vector2 direction)
    {
        _facingDirection = direction.normalized;
    }

    private void FixedUpdate()
    {
        _rb.velocity = _facingDirection * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Wall") || collider.gameObject.CompareTag("Player") || collider.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }
}
