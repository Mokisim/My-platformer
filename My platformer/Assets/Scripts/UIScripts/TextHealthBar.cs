using TMPro;
using UnityEngine;

public class TextHealthBar : HealthView
{
    [SerializeField] private TMP_Text _text;
    private float _maxHealth;

    public override void UpdateHealth(float targetValue)
    {
        _text.text = $"{targetValue}/{_maxHealth}";
    }

    public override void SetValues(float maxHealth, float currentHealth)
    {
        _text.text = $"{currentHealth}/{maxHealth}";
        _maxHealth = maxHealth;
    }
}
