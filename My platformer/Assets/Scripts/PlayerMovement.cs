using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Transform))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Transform _playerTransform;

    private FallMultiplying _fallMultiplier;
    private ApplyingLinearDrag _applyingLinearDrag;

    private bool _facingRight = true;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }



    private void Flip()
    {
        _facingRight = !_facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
