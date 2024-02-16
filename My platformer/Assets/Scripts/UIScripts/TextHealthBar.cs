using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextHealthBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private PlayerHealth _player;
    private float _maxHealth;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerHealth>();
        _maxHealth = _player.CurrentHealth;
        SetTextValues();
    }

    private void OnEnable()
    {
        _player.HealthChanged += SetTextValues;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= SetTextValues;
    }

    private void SetTextValues()
    {
        _text.text = $"{_player.CurrentHealth}/{_maxHealth}";
    }
}
