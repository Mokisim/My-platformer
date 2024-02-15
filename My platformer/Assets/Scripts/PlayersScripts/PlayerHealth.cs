using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private const string RespawnHash = "Respawn";
    
    public float CurrentHealth { get; private set; }
    
    private GameObject _playerSpawn;
    private float _maxHealth = 3;
    private HealthBar _healthBar;
    private SmoothHealthbar _smoothHealthbar;
    private TextHealthBar _textHealthBar;

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    private void Start()
    {
        _playerSpawn = GameObject.FindWithTag(RespawnHash);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Cherry>(out Cherry cherry))
        {
            cherry.TryGetComponent<Item>(out Item item);
            item.DestroyWithSound();
            RestoreHealth();
        }
    }

    public void TakeDamage()
    {
        CurrentHealth--;


        transform.position = _playerSpawn.transform.position;
        Debug.Log(CurrentHealth.ToString());

        if (CurrentHealth <= 0)
        {
            Debug.Log("YOU LOST");
        }

        _healthBar.UpdateSliderValue();
        _smoothHealthbar.SmoothUpdateSliderValue();
        _textHealthBar.SetTextValues();
    }

    public void RestoreHealth()
    {
        if (CurrentHealth < _maxHealth)
        {
            CurrentHealth++;
        }

        Debug.Log(CurrentHealth.ToString());

        _healthBar.UpdateSliderValue();
        _smoothHealthbar.SmoothUpdateSliderValue();
        _textHealthBar.SetTextValues();
    }

    public void GetHealthBar(HealthBar healthBar)
    {
        _healthBar = healthBar;
        Debug.Log($"Передал значение для обычного бара, {_healthBar}");
    }

    public void GetHealthBar(SmoothHealthbar smoothHealthbar)
    {
        _smoothHealthbar = smoothHealthbar;
        Debug.Log($"Передал значение для плавного бара, {_smoothHealthbar}");
    }

    public void GetHealthBar(TextHealthBar textHealthBar)
    {
        _textHealthBar = textHealthBar;
        Debug.Log($"Передал значение для текстового бара, {_textHealthBar}");
    }
}
