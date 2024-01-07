using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class DinoEnemyScript : MonoBehaviour
{
    [SerializeField] private Transform _path;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Transform[] _points;
    private float _speed = 4f;
    private int _currentPoint;
    private bool _facingRight = true;
    private bool _isRunning;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        Move();
        ControlAnimations();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage();
        }
    }

    private void Move()
    {
        Transform target = _points[_currentPoint];

        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            _currentPoint++;

            if (_currentPoint >= _points.Length)
            {
                _currentPoint = 0;
            }
        }

        if (transform.position.x > target.position.x && !_facingRight)
        {
            Flip();
        }
        else if (transform.position.x < target.position.x && _facingRight)
        {
            Flip();
        }
    }

    private void ControlAnimations()
    {
        _animator.SetBool("isRunning", true);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
