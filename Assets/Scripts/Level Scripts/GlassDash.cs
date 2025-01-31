using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsDash : MonoBehaviour
{
    public int dashHitsToBreak = 3;
    private int currentDashHits = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.LeftShift))
        {
            currentDashHits++; 

            if (currentDashHits >= dashHitsToBreak)
            {
                Destroy(gameObject); 
            }
        }
    }
}
