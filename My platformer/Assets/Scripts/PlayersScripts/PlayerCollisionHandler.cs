using System;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public event Action<float> HealthHealed;
    public event Action CristalPicked;

    private float _healValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Gem>(out Gem gem))
        {
            gem.DestroyWithSound();
            CristalPicked?.Invoke();
        }
        else if (collision.TryGetComponent<Cherry>(out Cherry cherry))
        {
            cherry.DestroyWithSound();
            HealthHealed?.Invoke(_healValue);
        }
    }
}
