using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    private PlayerHealth _health;
    protected bool IsOnEnable;
    
    private void Awake()
    {
        SetValues(_health.CurrentHealth, _health.CurrentHealth);
    }

    private void OnEnable()
    {
        IsOnEnable = true;
        _health.HealthChanged += UpdateHealth;
    }

    private void OnDisable()
    {
        IsOnEnable = false;
        _health.HealthChanged -= UpdateHealth;
    }

    public abstract void UpdateHealth(float targetValue);

    public virtual void SetValues(float maxHealth, float currentHealth) { }
}
