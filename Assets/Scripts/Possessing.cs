using System.Collections.Generic;
using UnityEngine;

public class Possessing : MonoBehaviour
{
    private List<GameObject> _enemiesInRange = new List<GameObject>();
    private Dictionary<GameObject, GameObject> _outlines = new Dictionary<GameObject, GameObject>();

    private void Update()
    {
        Vector2 mousePosition = InputManager.MousePosition;

        foreach (var enemy in _enemiesInRange)
        {
            Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
            if (enemyCollider != null && enemyCollider.OverlapPoint(mousePosition))
            {
                ShowOutline(enemy, true);
            }
            else
            {
                ShowOutline(enemy, false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !_enemiesInRange.Contains(other.gameObject))
        {
            _enemiesInRange.Add(other.gameObject);
            CreateOutline(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && _enemiesInRange.Contains(other.gameObject))
        {
            ShowOutline(other.gameObject, false);
            RemoveOutline(other.gameObject);
            _enemiesInRange.Remove(other.gameObject);
        }
    }

    private void CreateOutline(GameObject enemy)
    {
        if (!_outlines.ContainsKey(enemy))
        {
            GameObject outline = new GameObject("Outline");
            SpriteRenderer enemySpriteRenderer = enemy.GetComponent<SpriteRenderer>();
            SpriteRenderer outlineRenderer = outline.AddComponent<SpriteRenderer>();

            outlineRenderer.sprite = enemySpriteRenderer.sprite;
            outlineRenderer.color = new Color(1f, 1f, 1f, 1f);
            outlineRenderer.sortingOrder = enemySpriteRenderer.sortingOrder - 1;

            outline.transform.SetParent(enemy.transform);
            outline.transform.localPosition = Vector3.zero;

            float outlineScaleFactor = 1.2f;
            outline.transform.localScale = Vector3.one * outlineScaleFactor;

            _outlines[enemy] = outline;
        }
    }

    private void ShowOutline(GameObject enemy, bool show)
    {
        if (_outlines.ContainsKey(enemy))
        {
            _outlines[enemy].SetActive(show);
        }
    }

    private void RemoveOutline(GameObject enemy)
    {
        if (_outlines.ContainsKey(enemy))
        {
            Destroy(_outlines[enemy]);
            _outlines.Remove(enemy);
        }
    }
}
