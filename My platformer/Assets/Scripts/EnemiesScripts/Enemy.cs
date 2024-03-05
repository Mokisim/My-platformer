using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.HealthOver += DestroyEnemy;
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
