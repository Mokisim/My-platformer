using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool OnGround { get; private set; }
    private Vector3 _groundRaycastOffset = new Vector3(0.5f, 0, 0);
    private float _groundRaycastLength = 0.65f;

    public void CheckCollisions(LayerMask groundLayer, Transform playerTransform)
    {
        OnGround = Physics2D.Raycast(playerTransform.position + _groundRaycastOffset, Vector2.down, _groundRaycastLength, groundLayer) ||
        Physics2D.Raycast(playerTransform.position - _groundRaycastOffset, Vector2.down, _groundRaycastLength, groundLayer);
    }
}
