using UnityEngine;

public class ApplicationLinearDrag : MonoBehaviour
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
