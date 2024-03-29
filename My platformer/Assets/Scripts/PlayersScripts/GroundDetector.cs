using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Vector3 _groundRaycastOffset = new Vector3(0.2f, 0, 0);
    [SerializeField] private float _groundRaycastLength = 0.7f;
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private LayerMask _groundLayer;

    public bool IsOnGround { get; private set; }

    public void CheckGroundCollisions()
    {
        IsOnGround = Physics2D.Raycast(_groundCheckPoint.position + _groundRaycastOffset, Vector2.down, _groundRaycastLength, _groundLayer) ||
        Physics2D.Raycast(_groundCheckPoint.position - _groundRaycastOffset, Vector2.down, _groundRaycastLength, _groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(_groundCheckPoint.position + _groundRaycastOffset, _groundCheckPoint.position + _groundRaycastOffset + Vector3.down * _groundRaycastLength);
        Gizmos.DrawLine(_groundCheckPoint.position - _groundRaycastOffset, _groundCheckPoint.position - _groundRaycastOffset + Vector3.down * _groundRaycastLength);
    }
}
