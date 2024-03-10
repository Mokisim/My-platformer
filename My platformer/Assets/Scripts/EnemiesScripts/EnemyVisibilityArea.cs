using System;
using UnityEngine;

public class EnemyVisibilityArea : MonoBehaviour
{
    public Action<Player> PlayerNoticed;

    public bool IsPlayer {  get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            IsPlayer = true;
            PlayerNoticed?.Invoke(player);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            IsPlayer = false;
            PlayerNoticed?.Invoke(player);
        }
    }
}
