using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class AnimationSwitcher : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    private int _isRunningHash = Animator.StringToHash("isRunning");
    private int _isGroundedHash = Animator.StringToHash("isGrounded");

    private void Awake()
    {
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
        _animator.SetBool(_isGroundedHash, _rigidbody2D.velocity.y == 0);
    }

    private void TurnOnRunAnimation()
    {
        _animator.SetBool(_isRunningHash, _rigidbody2D.velocity.x != 0);
    }
}
