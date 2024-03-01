using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<float> HealthChanged;
    public event Action HealthDecreased;
    public event Action HealthIncreased;

    public float CurrentHealth {  get; private set; }

    [SerializeField]private float _maxHealth = 3;

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage()
    {
        CurrentHealth--;

        if (HealthChanged != null || HealthDecreased != null)
        {
            HealthChanged?.Invoke(CurrentHealth);
            HealthDecreased?.Invoke();
        }
    }

    public void RestoreHealth()
    {
        if (CurrentHealth < _maxHealth)
        {
            CurrentHealth++;
        }

        if (HealthChanged != null || HealthIncreased != null)
        {
            HealthChanged?.Invoke(CurrentHealth);
            HealthIncreased?.Invoke();
        }
    }
}
