using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(InputReader), typeof(PlayerMovement))]
public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] LayerMask _enemyLayers;
    [SerializeField] private float _damage = 1;
    private InputReader _inputReader;
    private PlayerMovement _playerMovement;
    private float _startAttackPosition;
    private float _wallAttackPosition;
    
    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _playerMovement = GetComponent<PlayerMovement>();
        _startAttackPosition = _attackPoint.localPosition.x;
        _wallAttackPosition = -_attackPoint.localPosition.x;
    }

    private void Update()
    {
        if(_playerMovement.IsWallSliding == true)
        {
            _attackPoint.localPosition = new Vector3(_wallAttackPosition, _attackPoint.localPosition.y);
        }
        else
        {
            _attackPoint.localPosition = new Vector3(_startAttackPosition, _attackPoint.localPosition.y);
        }

        if (_inputReader.GetAttackInput())
        {
            Attack();
        }
    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.TryGetComponent<Health>(out Health enemyHealth) == true)
            {
                enemyHealth.TakeDamage(_damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
