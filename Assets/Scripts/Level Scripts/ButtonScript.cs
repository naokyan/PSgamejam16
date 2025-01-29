using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color activatedColor = Color.green;
    public bool buttonActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !buttonActivated)
        {
            spriteRenderer.color = activatedColor;
            buttonActivated = true;
        }
    }
}
