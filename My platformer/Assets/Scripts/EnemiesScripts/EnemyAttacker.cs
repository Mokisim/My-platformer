using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    private float _damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Player>(out Player player) && _damage > 0)
        {
            player.GetComponent<Health>().TakeDamage(_damage);
        }
    }
}
