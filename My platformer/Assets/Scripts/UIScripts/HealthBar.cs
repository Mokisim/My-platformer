using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : HealthView
{
    [SerializeField] private Slider _slider;
    
    public override void UpdateHealth(float targetValue)
    {
        UpdateSliderValue(targetValue);
    }

    private void UpdateSliderValue(float targetValue)
    {
        _slider.value = targetValue;
    }

    public override void SetValues(float maxHealth, float currentHealth)
    {
        _slider.maxValue = maxHealth;
        _slider.minValue = 0;
        _slider.value = currentHealth;
    }
}
