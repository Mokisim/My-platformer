using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] LayerMask _enemyLayers;
    [SerializeField] private float _damage = 1;
    private InputReader _inputReader;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
    }

    private void Update()
    {
        if (_inputReader.GetAttackInput())
        {
            Attack();
        }
    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);

        foreach(Collider2D enemy in enemies)
        {
            enemy.TryGetComponent<Health>(out Health enemyHealth);
            enemyHealth.TakeDamage(_damage);
        }
    }
}
