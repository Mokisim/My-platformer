using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public bool IsWallRight {  get; private set; }
    private Vector3 _wallRaycastOffset = new Vector3(0.09f, 0.04f, 1);
    private float _wallRaycastLength = 0.53f;

    public bool IsWalled(Transform wallCheck, LayerMask wallLayer)
    {
        float circleRadius = 0.2f;

        return Physics2D.OverlapCircle(wallCheck.position, circleRadius, wallLayer);
    }

    public void CheckCollisions(Transform playerTransform, LayerMask wallLayer)
    {
        IsWallRight = Physics2D.Raycast(playerTransform.position + _wallRaycastOffset, Vector2.right, _wallRaycastLength, wallLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position + _wallRaycastOffset, transform.position + _wallRaycastOffset + Vector3.right * _wallRaycastLength);
    }
}
