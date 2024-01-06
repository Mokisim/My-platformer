using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationsController : MonoBehaviour
{
    private GroundCheck _groundCheck;
    private PlayerMovement _playerMovement;
    private Animator _animator;

    private void Awake()
    {
        _groundCheck = GetComponent<GroundCheck>();
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        TurnOnJumpAnimation();
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

    }

    private void TurnOnWallSlidingAnimation()
    {
        if(!_groundCheck.OnGround && _playerMovement.IsWallSliding)
        {
            
        }
    }
}
