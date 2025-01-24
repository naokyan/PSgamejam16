using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _shootingCooldown = 0.5f;
    [SerializeField] private SpriteRenderer _gun; 
    [SerializeField] private Transform _bulletSpawnPoint; 

    private Vector2 playerFacingDirection;
    private float _nextFireTime = 0f;

    private void Start()
    {
        if (_bulletSpawnPoint == null)
        {
            _bulletSpawnPoint = new GameObject("BulletSpawnPoint").transform;
            _bulletSpawnPoint.SetParent(transform);
        }
    }

    private void Update()
    {
        Vector2 mousePosition = InputManager.MousePosition;
        Vector2 aimDirection = (mousePosition - _rb.position).normalized;

        if (aimDirection != Vector2.zero)
        {
            playerFacingDirection = aimDirection;
        }

        if (InputManager.OnShootPressed.IsPressed() && Time.time >= _nextFireTime)
        {
            Fire();
        }

        if (_gun != null && _bulletSpawnPoint != null)
        {
            UpdateBulletPosition();
        }
    }

    private void UpdateBulletPosition()
    {
        if (_gun != null)
        {
            Vector3 localTopOffset = new Vector3(0, _gun.bounds.extents.y, 0);

            Vector3 worldTopPosition = _gun.transform.TransformPoint(localTopOffset);

            _bulletSpawnPoint.position = worldTopPosition;
        }
    }

    private void Fire()
    {
        GameObject bullet = ObjectPool.instance.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = _bulletSpawnPoint.position;

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.Initialize(playerFacingDirection);
            }

            bullet.SetActive(true);

            _nextFireTime = Time.time + _shootingCooldown;
        }
    }
}
