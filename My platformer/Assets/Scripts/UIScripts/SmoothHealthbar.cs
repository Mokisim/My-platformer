using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthbar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _changeSpeed = 10f;
    private PlayerHealth _player;
    private float _maxHealth;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerHealth>();
        _maxHealth = _player.CurrentHealth;
        SetSliderValues();
        _player.GetHealthBar(this);
    }

    public void SmoothUpdateSliderValue()
    {
        _slider.value = Mathf.MoveTowards(_slider.value, _player.CurrentHealth / _maxHealth * 100, _changeSpeed * Time.deltaTime);
    }

    private void SetSliderValues()
    {
        _slider.maxValue = _player.CurrentHealth / _maxHealth * 100;
        _slider.minValue = 0;
    }
}
