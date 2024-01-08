using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(Animator))]
public class AnimationControl : MonoBehaviour
{
    private GroundCheck _groundCheck;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _groundCheck = GetComponent<GroundCheck>();
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
        if (!_groundCheck.OnGround)
        {
            _animator.SetBool("isGrounded", false);
        }
        else
        {
            _animator.SetBool("isGrounded", true);
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

        _animator.SetBool("isRunning", isRunning);
    }
}
