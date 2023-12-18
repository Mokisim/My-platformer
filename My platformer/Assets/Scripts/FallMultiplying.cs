using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallMultiplying : MonoBehaviour
{
    private float _fallMultiplier = 7f;
    private float _lowJumpFallMultiplier = 2f;

    public void FallMultiply(Rigidbody2D rigidbody)
    {
        if (rigidbody.velocity.y < 0)
        {
            rigidbody.gravityScale = _fallMultiplier;
        }
        else if (rigidbody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rigidbody.gravityScale = _lowJumpFallMultiplier;
        }
        else
        {
            rigidbody.gravityScale = 1f;
        }
    }
}
