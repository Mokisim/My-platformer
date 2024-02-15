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
    }

    private void Start()
    {
        _maxHealth = _player.CurrentHealth;
        SetTextValues();
        _player.GetHealthBar(this);
    }

    public void SetTextValues()
    {
        _text.text = $"{_player.CurrentHealth}/{_maxHealth}";
    }
}
