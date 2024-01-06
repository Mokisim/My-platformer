using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    [SerializeField] private Vector3 _wallRaycastOffset;
    [SerializeField] private Vector3 _isWalledRightRaycastOffset;
    [SerializeField] private Vector3 _isWalledLeftRaycastOffset;
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private float _wallRaycastLength;
    [SerializeField] private float _isWalledRaycastLength;
    [SerializeField] private Transform _wallCheck;

    public bool IsWallRight { get { return _isWallRight; } private set { } }
    public bool IsWallLeft { get { return _isWallLeft; } private set { } }
    private bool _isWallRight;
    private bool _isWallLeft;

    public bool IsWalled { get { return _isWalled; } private set { } }
    private bool _isWalled;

    public void CheckWallCollisions()
    {
        _isWallRight = Physics2D.Raycast(_wallCheck.position + _wallRaycastOffset, Vector2.right, _wallRaycastLength, _wallLayer);

        _isWalled = Physics2D.Raycast(_wallCheck.position + _isWalledRightRaycastOffset, Vector2.right, _isWalledRaycastLength, _wallLayer) 
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
