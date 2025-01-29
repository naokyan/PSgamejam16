using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAlien : Door
{ 
   private SpriteRenderer spriteRenderer; 
    public Transform teleportTarget;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!GameManager.IsPossessing)
            { 
                if (spriteRenderer != null)
                {
                    Color color = spriteRenderer.color;
                    color.a = 0.2f;
                    spriteRenderer.color = color;
                }

                if (teleportTarget != null)
                {
                    other.transform.position = teleportTarget.position;
                }

                StartCoroutine(RestoreTransparency()); 
            }
            else
            {
                Debug.Log("This door must be opened by an alien");
            }
        }
    }

    private IEnumerator RestoreTransparency()
    {
        yield return new WaitForSeconds(0.3f);

        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 1f;
            spriteRenderer.color = color;
        }
    }

        private void Update()
        {
        base.Update();
       }
}
