using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private Vector3 _spawnPoint;

    private void Start()
    {
        _spawnPoint = transform.Find("Spawn Point").position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerSpawnPoint.SpawnPoint = _spawnPoint;
        }
    }
}
