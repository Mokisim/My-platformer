using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private const string PlayerHash = "Player";

    [SerializeField] private Slider _slider;
    [SerializeField] private bool _isSmooth;
    [SerializeField] private bool _isTextBar;
    [SerializeField] private TMP_Text _text;
    [SerializeField]private float _changeSpeed = 1f;
    private PlayerHealth _player;
    private float _maxHealth;

    private void Start()
    {
        _player = FindAnyObjectByType<PlayerHealth>();
        _maxHealth = _player.CurrentHealth;

        if (_slider != null)
        {
            SetSliderValues();
        }

        if (_text != null)
        {
            SetTextValues();
        }
    }

    private void SetSliderValues()
    {
        _slider.maxValue = _player.GetComponent<PlayerHealth>().CurrentHealth/_maxHealth * 100;
        _slider.minValue = 0;
    }

    public void UpdateSliderValue()
    {
        _slider.value = _player.GetComponent<PlayerHealth>().CurrentHealth / _maxHealth * 100;
    }

    public void SmoothUpdateSliderValue()
    {
        _slider.value = Mathf.MoveTowards(_slider.value, _player.GetComponent<PlayerHealth>().CurrentHealth / _maxHealth * 100, _changeSpeed * Time.deltaTime);
    }

    public void SetTextValues()
    {
        _text.text = $"{_player.GetComponent<PlayerHealth>().CurrentHealth}/{_maxHealth}";
    }
}
