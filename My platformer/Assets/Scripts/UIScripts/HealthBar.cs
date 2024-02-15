using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private PlayerHealth _player;
    private float _maxHealth;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerHealth>();
        _maxHealth = _player.CurrentHealth;
        SetSliderValues();
        _player.GetHealthBar(this);
    }

    public void UpdateSliderValue()
    {
        _slider.value = _player.CurrentHealth / _maxHealth * 100;
    }

    private void SetSliderValues()
    {
        _slider.maxValue = _player.CurrentHealth / _maxHealth * 100;
        _slider.minValue = 0;
    }
}
