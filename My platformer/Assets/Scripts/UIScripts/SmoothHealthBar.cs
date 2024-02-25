using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthBar : HealthView
{
    [SerializeField] private float _changeSpeed = 10f;
    [SerializeField] private Slider _slider;
    private PlayerHealth _player;
    private Coroutine _coroutine;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerHealth>();
        SetValues();
        GetHealthComponent(_player);
    }

    private void Start()
    {
        _coroutine = StartCoroutine(UpdateSmoothHealthBar(_player.CurrentHealth));
    }

    public override void UpdateHealth(float targetValue)
    {
        if(IsOnEnable == true)
        {
            StartSmoothUpdateHealthBar(targetValue);
        }
        else
        {
            StopSmoothUpdateHealthBar(targetValue);
        }
    }

    public override void SetValues()
    {
        _slider.maxValue = _player.CurrentHealth;
        _slider.minValue = 0;
        _slider.value = _player.CurrentHealth;
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
}
