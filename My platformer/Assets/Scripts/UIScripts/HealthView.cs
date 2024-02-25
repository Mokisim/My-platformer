using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    private PlayerHealth _health;
    
    private void Awake()
    {
        SetValues(_health.CurrentHealth, _health.CurrentHealth);
    }

    private void OnEnable()
    {
        _health.HealthChanged += UpdateHealth;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= UpdateHealth;
    }

    public abstract void UpdateHealth(float targetValue);

    public abstract void SetValues(float maxHealth, float currentHealth);

    public void GetPlayerHealth(PlayerHealth playerHealth)
    {
        _health = playerHealth;
    }
}
