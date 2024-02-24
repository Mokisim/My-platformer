using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    private PlayerHealth _health;

    private void OnEnable()
    {
        _health.HealthChanged += UpdateHealth;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= UpdateHealth;
    }

    public abstract void UpdateHealth(float targetValue);

    public virtual void SetValues() { }

    public void GetHealthComponent(PlayerHealth health)
    {
        _health = health;
    }
}
