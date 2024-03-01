using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Player>(out Player player))
        {
            player.GetComponent<Health>().TakeDamage();
        }
    }
}
