using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(InputControl))]
[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(WallCheck))]
[RequireComponent(typeof(ApplyingLinearDrag))]
[RequireComponent(typeof(FallMultiplying))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Transform _playerTransform;

    private InputControl _inputManager;
    private GroundCheck _groundCheck;
    private WallCheck _wallCheck;

    private FallMultiplying _fallMultiplier;
    private ApplyingLinearDrag _applyingLinearDrag;

    private bool _facingRight = true;
    private float _horizontalDirection;
    private float _horizontal;
    private float _movementSpeed = 5f;

    [Header("Jump")]
    [SerializeField] private int _jumpsValue = 1;
    private float _jumpForce = 13f;
    private int _aviableJumps;
    private float _hangTime = 0.2f;
    private float _hangTimeCounter;
    private float _jumpBufferLength = 0.1f;
    private float _jumpBufferCounter;
    private int _jumpInputChecker = 0;
    private bool _canJump => _aviableJumps > 0;

    [Header("Wall sliding and jumping")]
    [SerializeField] private Vector2 _wallJumpingPower = new Vector2(30f, 20f);
    public bool IsWallSliding { get { return _isWallSliding; } private set { } }
    private bool _isWallSliding;
    private float _wallSlidingSpeed = 2f;
    private bool _isWallJumping;
    private float _wallJumpingTime = 0.2f;
    private float _wallJumpingCounter = 1f;
    private float _wallJumpingDuration = 0.3f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerTransform = GetComponent<Transform>();
        _inputManager = GetComponent<InputControl>();
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

        if (_inputManager.GetJumpInput())
        {
            _jumpBufferCounter = _jumpBufferLength;
        }

        if (_aviableJumps > 0 && _hangTimeCounter > 0)
        {
            _hangTimeCounter -= Time.deltaTime;
        }

        Debug.Log(_canJump.ToString());
    }

    private void FixedUpdate()
    {
        WallSlide();
        WallJump();
        _groundCheck.CheckGroundCollisions();
        _wallCheck.CheckWallCollisions();

        if (_groundCheck.OnGround || !_isWallSliding || !_isWallJumping)
        {
            _rigidbody2D.velocity = new Vector2(_horizontal * _movementSpeed, _rigidbody2D.velocity.y);
        }

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

    private void WallSlide()
    {
        if (_wallCheck.IsWalled && !_groundCheck.OnGround && _inputManager.GetHorizontalAxisInput() != 0)
        {
            _isWallSliding = true;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Mathf.Clamp(_rigidbody2D.velocity.y, -_wallSlidingSpeed, float.MaxValue));
            _spriteRenderer.flipX = true;
        }
        else
        {
            _isWallSliding = false;
            _spriteRenderer.flipX = false;
        }
    }

    private void WallJump()
    {
        if (_isWallSliding)
        {
            _isWallJumping = false;
            _wallJumpingCounter = _wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            _wallJumpingCounter -= Time.deltaTime;
        }

        if (_inputManager.GetJumpInput() && _wallJumpingCounter > 0)
        {
            _isWallJumping = true;

            if (!_wallCheck.IsWallRight)
            {
                _rigidbody2D.velocity = new Vector2(_wallJumpingPower.x * 1, _wallJumpingPower.y);
            }
            else
            {
                _rigidbody2D.velocity = new Vector2(_wallJumpingPower.x * -1, _wallJumpingPower.y);
            }

            _wallJumpingCounter -= 1;

            Invoke(nameof(StopWallJumping), _wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        _isWallJumping = false;
    }

    private void Flip()
    {
        _facingRight = !_facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
