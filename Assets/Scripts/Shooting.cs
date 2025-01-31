using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private SpriteRenderer _gun; 
    [SerializeField] private Transform _bulletSpawnPoint;

    [SerializeField] private float _shootingCooldown = 0.5f;
    [SerializeField, Min(1)] private int _bulletsToShootEachTime = 1;
    [SerializeField] private float _bulletSpreadAngle = 30f;

    private Vector2 playerFacingDirection;
    private float _nextFireTime = 0f;

    private EnemyController _enemyScript;
    private Transform _player;

    private bool _isControlledByPlayer;

    [SerializeField] private float _muzzleXOffset;
    [SerializeField] private float _muzzleYOffset;

    private bool _hasBulletSpawn;
    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;

        if (_bulletSpawnPoint == null)
        {
            _bulletSpawnPoint = new GameObject("BulletSpawnPoint").transform;
            _bulletSpawnPoint.SetParent(transform);
        }
        else
        {
            _hasBulletSpawn = true;
        }

        if (GetComponentInParent<EnemyController>())
        {
            _enemyScript = GetComponentInParent<EnemyController>();
        }
    }

    private void Update()
    {
        //test if this is controlled by player or enemy
        if (GetComponent<PlayerMovement>().enabled)
        {
            _isControlledByPlayer = true;
            if (InputManager.OnShootPressed.IsPressed() && Time.time >= _nextFireTime)
            {
                playerFacingDirection = InputManager.MousePosition;
                Fire();
            }
        }
        else
        {
            _isControlledByPlayer = false;
            if (_enemyScript != null && _enemyScript.PlayerInRange() && Time.time >= _nextFireTime)
            {
                playerFacingDirection = (Vector2)_player.position;
                Fire();
            }
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
            Vector3 gunDirection = _gun.transform.right; 

            Vector3 muzzlePosition = _gun.transform.position + gunDirection * _muzzleXOffset + _gun.transform.up * _muzzleYOffset;

            if (!_hasBulletSpawn)
            {
                _bulletSpawnPoint.position = muzzlePosition;
            }
        }
    }

    private void Fire()
    {
        Vector2 baseAimDirection = (playerFacingDirection - _rb.position).normalized; 

        for (int i = 0; i < _bulletsToShootEachTime; i++)
        {
            GameObject bullet = ObjectPool.instance.GetPooledObject();

            if (bullet != null)
            {
                bullet.transform.position = _bulletSpawnPoint.position;

                float angleOffset = (i - (_bulletsToShootEachTime - 1) / 2f) * _bulletSpreadAngle;
                float angle = Mathf.Atan2(baseAimDirection.y, baseAimDirection.x) * Mathf.Rad2Deg + angleOffset;

                Vector2 bulletDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;

                bullet.transform.rotation = Quaternion.Euler(0, 0, angle - 90); 

                Bullet bulletScript = bullet.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.Initialize(bulletDirection); 
                }

                bullet.tag = _isControlledByPlayer ? "PlayerBullet" : "EnemyBullet";

                bullet.SetActive(true);
            }
        }

        _nextFireTime = Time.time + _shootingCooldown;
    }
}
