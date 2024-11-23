using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(InputReader))]
[RequireComponent(typeof(GroundDetector), typeof(WallDetector), typeof(ApplicationLinearDrag))]
[RequireComponent(typeof(FallMultiplyer))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Transform _playerTransform;

    private InputReader _inputManager;
    private GroundDetector _groundCheck;
    private WallDetector _wallCheck;

    private FallMultiplyer _fallMultiplier;
    private ApplicationLinearDrag _applyingLinearDrag;

    private bool _facingRight = true;
    private float _horizontalDirection;
    private float _movementSpeed = 5f;

    public bool FacingRight => _facingRight;

    [Header("Dash")]
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashTime = 0.4f;

    private bool _isDashing;
    private Coroutine _dashCoroutine;

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
    private bool _isWallSliding;
    private float _wallSlidingSpeed = 2f;
    private bool _isWallJumping;
    private float _wallJumpingTime = 0.2f;
    private float _wallJumpingCounter = 1f;
    private float _wallJumpingDuration = 0.3f;

    public bool IsWallSliding { get { return _isWallSliding; } private set { } }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _inputManager = GetComponent<InputReader>();
        _groundCheck = GetComponent<GroundDetector>();
        _wallCheck = GetComponent<WallDetector>();
        _applyingLinearDrag = GetComponent<ApplicationLinearDrag>();
        _fallMultiplier = GetComponent<FallMultiplyer>();
    }

    private void Update()
    {
        _horizontalDirection = _inputManager.GetHorizontalAxisInput();

        if (_inputManager.GetDashInput() && _isDashing == false)
        {
            if (_facingRight == true)
            {
                _dashCoroutine = StartCoroutine(Dash(1f));
            }
            else
            {
                _dashCoroutine = StartCoroutine(Dash(-1f));
            }
        }
        else if (_isDashing == false && _dashCoroutine != null)
        {
            StopCoroutine(_dashCoroutine);
        }

        if (_horizontalDirection > 0 && _facingRight == false || _horizontalDirection < 0 && _facingRight)
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

        WallSlide();
        WallJump();
    }

    private void FixedUpdate()
    {
        _groundCheck.CheckGroundCollisions();
        _wallCheck.CheckWallCollisions();

        if (_isDashing == false)
        {
            if (_groundCheck.IsOnGround || _isWallSliding == false || _isWallJumping == false)
            {
                _rigidbody2D.velocity = new Vector2(_horizontalDirection * _movementSpeed, _rigidbody2D.velocity.y);
            }

            if (_groundCheck.IsOnGround)
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
    }

    private void OnDisable()
    {
        if (_dashCoroutine != null)
        {
            StopCoroutine(_dashCoroutine);
        }
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0f);
        _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void WallSlide()
    {
        if (_wallCheck.IsWalled && _groundCheck.IsOnGround == false && _inputManager.GetHorizontalAxisInput() != 0)
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

    private IEnumerator Dash(float direction)
    {
        _isDashing = true;
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0f);
        _rigidbody2D.AddForce(new Vector2(_dashDistance * direction, 0f), ForceMode2D.Impulse);
        float gravity = _rigidbody2D.gravityScale;
        _rigidbody2D.gravityScale = 0;

        yield return new WaitForSeconds(_dashTime);

        _isDashing = false;
        _rigidbody2D.gravityScale = gravity;

        Debug.Log("dash");
    }

    private void Flip()
    {
        _facingRight = !_facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
