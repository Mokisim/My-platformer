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
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag(PlayerHash);

        if (_slider != null)
        {
            SetSliderValues();
        }

        if (_text != null)
        {
            SetTextValues();
        }
    }

    private void Update()
    {
        if (_isSmooth == true)
        {
            SmoothUpdateSliderValue();
        }
        else if (_isTextBar == true)
        {
            SetTextValues();
        }
        else
        {
            UpdateSliderValue();
        }
    }

    private void SetSliderValues()
    {
        _slider.maxValue = _player.GetComponent<PlayerHealth>().CurrentHealth/_player.GetComponent<PlayerHealth>().MaxHealth * 100;
        _slider.minValue = 0;
    }

    private void UpdateSliderValue()
    {
        _slider.value = _player.GetComponent<PlayerHealth>().CurrentHealth / _player.GetComponent<PlayerHealth>().MaxHealth * 100;
    }

    private void SmoothUpdateSliderValue()
    {
        _slider.value = Mathf.MoveTowards(_slider.value, _player.GetComponent<PlayerHealth>().CurrentHealth / _player.GetComponent<PlayerHealth>().MaxHealth * 100, _changeSpeed * Time.deltaTime);
    }

    private void SetTextValues()
    {
        _text.text = $"{_player.GetComponent<PlayerHealth>().CurrentHealth}/{_player.GetComponent<PlayerHealth>().MaxHealth}";
    }
}
