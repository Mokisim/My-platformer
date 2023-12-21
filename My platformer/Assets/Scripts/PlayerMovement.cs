using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(GroundCheck))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Transform _playerTransform;

    private InputManager _inputManager;
    private GroundCheck _groundCheck;

    private FallMultiplying _fallMultiplier;
    private ApplyingLinearDrag _applyingLinearDrag;

    private bool _facingRight = true;
    private float _horizontalDirection;
    private float _horizontal;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerTransform = GetComponent<Transform>();
        _inputManager = GetComponent<InputManager>();
        _groundCheck = GetComponent<GroundCheck>();
    }

    private void Update()
    {
        _horizontalDirection = _inputManager.GetInput().x;
        _horizontal = _inputManager.GetHorizontalAxisInput();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_horizontal * 10f, _rigidbody2D.velocity.y);

        _groundCheck.CheckGroundCollisions();
        Debug.Log($"{_groundCheck.OnGround}");
    }

    private void Flip()
    {
        _facingRight = !_facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
