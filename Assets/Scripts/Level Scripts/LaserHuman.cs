using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class LaserHuman : Laser
{
    [SerializeField] private GameObject _boss;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_boss.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
}

