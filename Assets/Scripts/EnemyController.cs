using System.Security.Cryptography;
using TMPro; // Add this to use TextMeshPro
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _detectionRadius = 10f;

    private Rigidbody2D _rb;
    private LayerMask _playerLayer;
    private Collider2D _collided;

    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _wanderRadius = 3f;
    [SerializeField] private float _wanderInterval = 2f;
    private Vector2 _startingPosition;
    private Vector2 _wanderTarget;
    private float _wanderTimer;

    private Animator _animator;
    private const string _aimHorizontal = "AimHorizontal";
    private const string _aimVertical = "AimVertical";
    private const string _aimLastHorizontal = "AimLastHorizontal";
    private const string _aimLastVertical = "AimLastVertical";

    private const string _moveHorizontal = "MoveHorizontal";
    private const string _moveVertical = "MoveVertical";

    private Vector2 _direction;

    private Transform _player;

    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody2D>();

        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Possessing>().enabled = false;

        _playerLayer = LayerMask.GetMask("Player");

        _animator = GetComponent<Animator>();

        _player = GameObject.FindWithTag("Player").transform;

        _startingPosition = transform.position;
        SetNewWanderTarget();
    }

    private void Update()
    {
        EnemyBehave();
        UpdateAnimation();

        /*
        if (GameManager.NewGame)
        {
            transform.position = _startingPosition;
        }*/
    }

    public bool PlayerInRange()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _detectionRadius, _playerLayer);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                _collided = collider;
                return true;
            }
        }
        return false;
    }

    private void EnemyBehave()
    {
        if (PlayerInRange())
        {
            _direction = (_collided.transform.position - transform.position).normalized;
            _rb.velocity = _direction * _moveSpeed;
        }
        else
        {
            Wander();
        }
    }

    private void Wander()
    {
        _direction = (_wanderTarget - (Vector2)transform.position).normalized;
        _rb.velocity = _direction * _moveSpeed;

        if (Vector2.Distance(transform.position, _wanderTarget) < 0.1f)
        {
            SetNewWanderTarget();
        }

        _wanderTimer -= Time.deltaTime;
        if (_wanderTimer <= 0)
        {
            SetNewWanderTarget();
        }
    }

    private void UpdateAnimation()
    {
        _animator.SetFloat(_moveHorizontal, _direction.x);
        _animator.SetFloat(_moveVertical, _direction.y);

        float aimXForAnimation = Mathf.Abs(_direction.x);
        float aimYForAnimation = _direction.y;

        _animator.SetFloat(_aimHorizontal, aimXForAnimation);
        _animator.SetFloat(_aimVertical, aimYForAnimation);

        if (_direction != Vector2.zero)
        {
            _animator.SetFloat(_aimLastHorizontal, _direction.x);
            _animator.SetFloat(_aimLastVertical, _direction.y);
        }

        if (_direction.x < 0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (_direction.x > 0f)
        {
            GetComponent<SpriteRenderer>().flipX = false;

        }
    }

    private void SetNewWanderTarget()
    {
        _wanderTarget = _startingPosition + UnityEngine.Random.insideUnitCircle * _wanderRadius;
        _wanderTimer = _wanderInterval;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_startingPosition, _wanderRadius);
    }
}
