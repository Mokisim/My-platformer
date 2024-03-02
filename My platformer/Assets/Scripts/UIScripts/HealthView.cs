using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;

    private void Start()
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
}
