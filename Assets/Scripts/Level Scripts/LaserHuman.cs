using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class LaserHuman : Laser
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && GameManager.IsPossessing)
        {
            gameObject.SetActive(false);
        }
    }
}

