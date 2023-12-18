using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyingLinearDrag : MonoBehaviour
{
    private float _groundLinearDrag = 5f;
    private float _airLinearDrag = 5f;

    public void ApplyGroundLinearDrag(Rigidbody2D rigidbody)
    {
        rigidbody.drag = _groundLinearDrag;
    }

    public void ApplyAirLinearDrag(Rigidbody2D rigidbody)
    {
        rigidbody.drag = _airLinearDrag;
    }
}
