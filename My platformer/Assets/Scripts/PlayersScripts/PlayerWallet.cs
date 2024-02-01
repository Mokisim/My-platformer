using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Gem>(out Gem gem))
        {
            gem.TryGetComponent<Item>(out Item item);
            item.DestroyWithSound();
        }
    }
}
