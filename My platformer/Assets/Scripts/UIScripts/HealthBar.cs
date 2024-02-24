using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float _changeSpeed = 0.5f;
    [SerializeField] private Slider _slider;
    [SerializeField] private bool _isSmooth;
    private PlayerHealth _player;
    private Coroutine _coroutine;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerHealth>();
        SetSliderValues();
    }

    private void Start()
    {
        _coroutine = StartCoroutine(UpdateSmoothHealthBar(_player.CurrentHealth));
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

    private IEnumerator UpdateSmoothHealthBar(float targetValue)
    {
        while (_slider.value != _player.CurrentHealth)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, targetValue, _changeSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private void StartSmoothUpdateHealthBar(float targetValue)
    {
        StartCoroutine(UpdateSmoothHealthBar(targetValue));
    }

    private void StopSmoothUpdateHealthBar(float t)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private void UpdateSliderValue(float targetValue)
    {
        _slider.value = targetValue;
    }

    private void SetSliderValues()
    {
        _slider.maxValue = _player.CurrentHealth;
        _slider.minValue = 0;
        _slider.value = _player.CurrentHealth;
    }
}
