using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(WallCheck))]
[RequireComponent(typeof(ApplyingLinearDrag))]
[RequireComponent(typeof(FallMultiplying))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Transform _playerTransform;

    private InputManager _inputManager;
    private GroundCheck _groundCheck;
    private WallCheck _wallCheck;

    private FallMultiplying _fallMultiplier;
    private ApplyingLinearDrag _applyingLinearDrag;

    private bool _facingRight = true;
    private float _horizontalDirection;
    private float _horizontal;
    private float _movementSpeed = 5f;

    [SerializeField] private int _jumpsValue = 1;
    private float _jumpForce = 13f;
    private int _aviableJumps;
    private float _hangTime = 0.2f;
    private float _hangTimeCounter;
    private float _jumpBufferLength = 0.1f;
    private float _jumpBufferCounter;
    private int _jumpInputChecker = 0;
    private bool _canJump => _jumpBufferCounter > 0f && _aviableJumps > 0;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerTransform = GetComponent<Transform>();
        _inputManager = GetComponent<InputManager>();
        _groundCheck = GetComponent<GroundCheck>();
        _wallCheck = GetComponent<WallCheck>();
        _applyingLinearDrag = GetComponent<ApplyingLinearDrag>();
        _fallMultiplier = GetComponent<FallMultiplying>();
    }

    private void Update()
    {
        _horizontalDirection = _inputManager.GetInput().x;
        _horizontal = _inputManager.GetHorizontalAxisInput();

        if (_horizontal > 0 && !_facingRight)
        {
            Flip();
        }
        else if (_horizontal < 0 && _facingRight)
        {
            Flip();
        }

        if (_inputManager.GetJumpInput())
        {
            _jumpBufferCounter = _jumpBufferLength;
        }

        if (_canJump && _inputManager.GetJumpInput())
        {
            if ((_hangTimeCounter <= 0) && (_aviableJumps == _jumpsValue))
            {
                _aviableJumps--;
            }

            if (_aviableJumps > 0)
            {
                Jump();
                _jumpInputChecker = 0;
                _aviableJumps--;
                _hangTimeCounter = 0f;
            }
        }

        if (_aviableJumps > 0 && _hangTimeCounter > 0)
        {
            _hangTimeCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_horizontal * _movementSpeed, _rigidbody2D.velocity.y);

        _groundCheck.CheckGroundCollisions();
        _wallCheck.CheckWallCollisions();

        if (_groundCheck.OnGround)
        {
            _applyingLinearDrag.ApplyGroundLinearDrag(_rigidbody2D);
            
            if (_jumpInputChecker > 0)
            {
                _hangTimeCounter = _hangTime;
                _aviableJumps = _jumpsValue;
            }
        }
        else
        {
            _applyingLinearDrag.ApplyAirLinearDrag(_rigidbody2D);
            _fallMultiplier.FallMultiply(_rigidbody2D);
            _jumpBufferCounter -= Time.deltaTime;
            _jumpInputChecker = 1;
        }
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0f);
        _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
