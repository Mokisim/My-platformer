using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : HealthView
{
    [SerializeField] private float _changeSpeed = 0.5f;
    [SerializeField] private Slider _slider;
    [SerializeField] private bool _isSmooth;
    private PlayerHealth _player;
    private Coroutine _coroutine;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerHealth>();
        SetValues();
        GetHealthComponent(_player);
    }

    public override void UpdateHealth(float targetValue)
    {
        UpdateSliderValue(targetValue);
    }

    private void UpdateSliderValue(float targetValue)
    {
        _slider.value = targetValue;
    }

    public override void SetValues()
    {
        _slider.maxValue = _player.CurrentHealth;
        _slider.minValue = 0;
        _slider.value = _player.CurrentHealth;
    }
}
