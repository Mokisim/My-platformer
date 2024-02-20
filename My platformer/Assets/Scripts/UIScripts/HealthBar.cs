using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float _changeSpeed = 100f;
    [SerializeField] private Slider _slider;
    [SerializeField] private bool _isSmooth;
    private PlayerHealth _player;
    private float _maxHealth;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerHealth>();
    }

    private void Start()
    {
        _maxHealth = _player.CurrentHealth;
        SetSliderValues();
    }

    private void OnEnable()
    {
        if (_isSmooth == true)
        {
            _player.HealthChanged += StartSmoothUpdateHealthBar;
        }
        else
        {
            _player.HealthChanged += UpdateSliderValue;
        }
    }

    private void OnDisable()
    {
        if (_isSmooth == true)
        {
            _player.HealthChanged -= StopSmoothUpdateHealthBar;
        }
        else
        {
            _player.HealthChanged -= UpdateSliderValue;
        }
    }

    private IEnumerator UpdateSmoothHealthBar()
    {
        while (_slider.value != _player.CurrentHealth / _maxHealth * 100)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _player.CurrentHealth / _maxHealth * 100, _changeSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private void StartSmoothUpdateHealthBar()
    {
        StartCoroutine(UpdateSmoothHealthBar());
    }

    private void StopSmoothUpdateHealthBar()
    {
        StopCoroutine(UpdateSmoothHealthBar());
    }

    private void UpdateSliderValue()
    {
        _slider.value = _player.CurrentHealth / _maxHealth * 100;
    }

    private void SetSliderValues()
    {
        _slider.maxValue = _player.CurrentHealth / _maxHealth * 100;
        _slider.minValue = 0;
        _slider.value = _player.CurrentHealth / _maxHealth * 100;
    }
}
