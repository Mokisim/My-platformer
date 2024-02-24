using TMPro;
using UnityEngine;

public class TextHealthBar : HealthView
{
    [SerializeField] private TMP_Text _text;
    private PlayerHealth _player;
    private float _maxHealth;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerHealth>();
        _maxHealth = _player.CurrentHealth;
        GetHealthComponent(_player);
        SetValues();
    }

    public override void UpdateHealth(float targetValue)
    {
        _text.text = $"{targetValue}/{_maxHealth}";
    }

    public override void SetValues()
    {
        _text.text = $"{_player.CurrentHealth}/{_maxHealth}";
    }
}
