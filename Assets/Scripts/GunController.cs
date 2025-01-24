using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private Transform _player;
    [SerializeField] private Transform _rightHand;
    [SerializeField] private Transform _leftHand;

    [SerializeField] private SpriteRenderer _playerSpriteRenderer;

    private SpriteRenderer _gunSpriteRenderer;

    private Transform _currentHand;
    
    private void Awake()
    {
        _gunSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 mousePosition = InputManager.MousePosition;
        Vector2 aimDirection = (mousePosition - _rb.position).normalized;

        if (mousePosition.x > _player.position.x)
        {
            _currentHand = _rightHand;
        }
        else
        {
            _currentHand = _leftHand;
        }

        transform.position = _currentHand.position;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Vector3 localScale = transform.localScale;
        localScale.x = (_currentHand == _leftHand) ? -1 : 1;
        transform.localScale = localScale;

        if (mousePosition.y > _player.position.y) 
        {
            _gunSpriteRenderer.sortingOrder = _playerSpriteRenderer.sortingOrder - 1;
        }
        else
        {
            _gunSpriteRenderer.sortingOrder = _playerSpriteRenderer.sortingOrder + 1;
        }
    }
}
