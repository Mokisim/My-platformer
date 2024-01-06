using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    private GroundCheck _groundCheck;
    [SerializeField]private Animator _animator;

    private void Awake()
    {
        _groundCheck = GetComponent<GroundCheck>();
        _animator = GetComponent<Animator>();
    }

    public void TurnOnJumpAnimation()
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
}
