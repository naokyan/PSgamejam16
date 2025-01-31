using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    public static PlayerSpawnPoint Instance;

    public static Vector3 SpawnPoint;
    public static bool GameFirstStart;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Ensure there's only one instance
        }

        if (!GameFirstStart)
        {
            SpawnPoint = GameObject.FindWithTag("Player").transform.position;
        }
    }
}