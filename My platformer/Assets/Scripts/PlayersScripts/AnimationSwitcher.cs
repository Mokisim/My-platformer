using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputReader))]
[RequireComponent(typeof(Animator))]
public class AnimationSwitcher : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private InputReader _inputReader;

    private int _isRunningHash = Animator.StringToHash("isRunning");
    private int _isGroundedHash = Animator.StringToHash("isGrounded");
    private int _isAttackHash = Animator.StringToHash("attack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _inputReader = GetComponent<InputReader>();
    }

    private void Update()
    {
        TurnOnJumpAnimation();
        TurnOnRunAnimation();

        if (_inputReader.GetAttackInput())
        {
            TurnOnAttackAnimation();
        }
    }

    private void TurnOnJumpAnimation()
    {
        _animator.SetBool(_isGroundedHash, _rigidbody2D.velocity.y == 0);
    }

    private void TurnOnRunAnimation()
    {
        _animator.SetBool(_isRunningHash, _rigidbody2D.velocity.x != 0);
    }

    private void TurnOnAttackAnimation()
    {
        _animator.SetTrigger(_isAttackHash);
    }
}
