using UnityEngine;

public class WallDetector : MonoBehaviour
{
    [SerializeField] private Vector3 _wallRaycastOffset;
    [SerializeField] private Vector3 _isWalledRightRaycastOffset;
    [SerializeField] private Vector3 _isWalledLeftRaycastOffset;
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private float _wallRaycastLength;
    [SerializeField] private float _isWalledRaycastLength;
    [SerializeField] private Transform _wallCheck;

    public bool IsWallRight { get; private set; }
    public bool IsWalled { get; private set; }

    public void CheckWallCollisions()
    {
        IsWallRight = Physics2D.Raycast(_wallCheck.position + _wallRaycastOffset, Vector2.right, _wallRaycastLength, _wallLayer);

        IsWalled = Physics2D.Raycast(_wallCheck.position + _isWalledRightRaycastOffset, Vector2.right, _isWalledRaycastLength, _wallLayer)
            || Physics2D.Raycast(_wallCheck.position - _isWalledLeftRaycastOffset, Vector2.left, _isWalledRaycastLength, _wallLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_wallCheck.position + _wallRaycastOffset, _wallCheck.position + _wallRaycastOffset + Vector3.right * _wallRaycastLength);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(_wallCheck.position + _isWalledRightRaycastOffset, _wallCheck.position + _isWalledRightRaycastOffset + Vector3.right * _isWalledRaycastLength);
        Gizmos.DrawLine(_wallCheck.position - _isWalledLeftRaycastOffset, _wallCheck.position - _isWalledLeftRaycastOffset + Vector3.left * _isWalledRaycastLength);
    }
}
