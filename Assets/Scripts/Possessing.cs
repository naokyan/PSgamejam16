using System.Collections.Generic;
using UnityEngine;

public class Possessing : MonoBehaviour
{
    [SerializeField] private float _detectionRadius = 10f;
    
    private LayerMask _enemyLayer; 

    private List<GameObject> _enemiesInRange = new List<GameObject>();
    private Dictionary<GameObject, GameObject> _outlines = new Dictionary<GameObject, GameObject>();

    private void Start()
    {
        _enemyLayer = LayerMask.GetMask("Enemy");
    }

    private void Update()
    {
        DetectEnemiesInRange();

        Vector2 mousePosition = InputManager.MousePosition;

        foreach (GameObject enemy in _enemiesInRange)
        {
            Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
            if (enemyCollider != null && enemyCollider.OverlapPoint(mousePosition))
            {
                ShowOutline(enemy, true);

                if (InputManager.OnPossessPressed.WasPressedThisFrame() && GameManager.CanPossess)
                {
                    GameManager.PossessEnemy(enemy);
                    return;
                }
            }
            else
            {
                ShowOutline(enemy, false);
            }
        }

        if (InputManager.OnPossessPressed.WasPressedThisFrame() && GameManager.IsPossessing)
        {
            GameManager.BackToOriginalForm();
        }
    }

    private void DetectEnemiesInRange()
    {
        _enemiesInRange.Clear();

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _detectionRadius, _enemyLayer);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Enemy") && !_enemiesInRange.Contains(collider.gameObject))
            {
                _enemiesInRange.Add(collider.gameObject);
                CreateOutline(collider.gameObject);
            }
        }

        List<GameObject> enemiesToRemove = new List<GameObject>();
        foreach (GameObject enemy in _enemiesInRange)
        {
            if (enemy == null || Vector2.Distance(transform.position, enemy.transform.position) > _detectionRadius)
            {
                enemiesToRemove.Add(enemy);
            }
        }

        foreach (GameObject enemy in enemiesToRemove)
        {
            ShowOutline(enemy, false);
            RemoveOutline(enemy);
            _enemiesInRange.Remove(enemy);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}