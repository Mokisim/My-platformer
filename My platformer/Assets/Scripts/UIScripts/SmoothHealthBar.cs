using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthBar : HealthView
{
    [SerializeField] private float _changeSpeed = 10f;
    [SerializeField] private Slider _slider;
    private Coroutine _coroutine;

    public override void UpdateHealth(float targetValue)
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(UpdateSmoothHealthBar(targetValue));
    }

    public override void SetValues(float maxHealth, float currentHealth)
    {
        _slider.maxValue = maxHealth;
        _slider.minValue = 0;
        _slider.value = currentHealth;
    }

    private IEnumerator UpdateSmoothHealthBar(float targetValue)
    {
        while (_slider.value != targetValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, targetValue, _changeSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
