using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsDash : MonoBehaviour
{
    public int dashHitsToBreak = 3;
    private int currentDashHits = 0;

    public SpriteRenderer spriteRenderer;
    public Sprite[] damageSprites;

    public GameObject brokenWindow;

    private void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.LeftShift))
        {
            currentDashHits++;

            if (currentDashHits < dashHitsToBreak && damageSprites.Length > 0)
            {
                int spriteIndex = Mathf.Clamp(currentDashHits - 1, 0, damageSprites.Length - 1);
                spriteRenderer.sprite = damageSprites[spriteIndex];
            }
            else if (currentDashHits >= dashHitsToBreak)
            {
                if (brokenWindow!= null)
                {
                    Instantiate(brokenWindow, transform.position, transform.rotation);
                }

                Destroy(gameObject);
            }
        }
    }
} 
