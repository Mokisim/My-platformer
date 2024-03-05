using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 3;
    private float _minHealth = 0;

    public event Action<float> HealthChanged;
    public event Action HealthDecreased;
    public event Action HealthIncreased;
    public event Action HealthOver;

    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, _minHealth, _maxHealth);

            if (HealthChanged != null || HealthDecreased != null)
            {
                HealthChanged?.Invoke(CurrentHealth);
                HealthDecreased?.Invoke();
            }

            if (HealthOver != null && CurrentHealth == 0)
            {
                HealthOver?.Invoke();
            }
        }

        Debug.Log("Я получил урон");
    }

    public void RestoreHealth(float heal)
    {
        if (heal > 0)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + heal, _minHealth, _maxHealth);

            if (HealthChanged != null || HealthIncreased != null)
            {
                HealthChanged?.Invoke(CurrentHealth);
                HealthIncreased?.Invoke();
            }
        }
    }
}
