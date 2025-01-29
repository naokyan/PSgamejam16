using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DoorHuman : Door
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.IsPossessing)
            {
                isOpening = true;
            }
            else
            {
                Debug.Log("This door must be opened by a human");
            }
        }
    }
}
