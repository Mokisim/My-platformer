using System;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public event Action HealthHealed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Gem>(out Gem gem))
        {
            gem.TryGetComponent<Item>(out Item item);
            item.DestroyWithSound();
        }
        else if (collision.TryGetComponent<Cherry>(out Cherry cherry))
        {
            cherry.TryGetComponent<Item>(out Item item);
            item.DestroyWithSound();
            HealthHealed?.Invoke();
        }
    }
}