using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform bulletPosition;
    private Vector2 playerFacingDirection;

    private void Update()
    {
        if (InputManager.Movement != Vector2.zero)
        {
            playerFacingDirection = InputManager.Movement.normalized;
        }

        if (InputManager.OnShootPressed.WasPressedThisFrame())
        {
            Fire();
        }
    }

    private void Fire()
    {
        GameObject bullet = ObjectPool.instance.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = bulletPosition.position;

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.Initialize(playerFacingDirection);
            }

            bullet.SetActive(true);
        }
    }
}
