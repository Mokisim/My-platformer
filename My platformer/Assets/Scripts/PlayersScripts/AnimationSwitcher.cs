using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(Animator))]
public class AnimationSwitcher : MonoBehaviour
{
    private GroundDetector _groundCheck;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    private int _isRunningHash = Animator.StringToHash("isRunning");
    private int _isGroundedHash = Animator.StringToHash("isGrounded");

    private void Awake()
    {
        _groundCheck = GetComponent<GroundDetector>();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        TurnOnJumpAnimation();
        TurnOnRunAnimation();
    }

    private void TurnOnJumpAnimation()
    {
        if (_groundCheck.OnGround == false)
        {
            _animator.SetBool(_isGroundedHash, false);
        }
        else
        {
            _animator.SetBool(_isGroundedHash, true);
        }
    }

    private void TurnOnRunAnimation()
    {
        bool isRunning;

        if (_rigidbody2D.velocity.x != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        _animator.SetBool(_isRunningHash, isRunning);
    }
}