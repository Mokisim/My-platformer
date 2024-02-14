using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private const string RespawnHash = "Respawn";
    private const string CanvasHash = "Canvas";

    public float CurrentHealth { get; private set; }
    
    private GameObject _playerSpawn;
    private float _maxHealth = 3;
    private HealthBar _healthBar;

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    private void Start()
    {
        _playerSpawn = GameObject.FindWithTag(RespawnHash);
        _healthBar = GameObject.FindWithTag(CanvasHash).GetComponent<HealthBar>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Cherry>(out Cherry cherry))
        {
            cherry.TryGetComponent<Item>(out Item item);
            item.DestroyWithSound();
            RestoreHealth();
            _healthBar.UpdateSliderValue();
            _healthBar.SmoothUpdateSliderValue();
            _healthBar.SetTextValues();
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
        _healthBar.SmoothUpdateSliderValue();
        _healthBar.SetTextValues();
    }

    public void RestoreHealth()
    {
        if (CurrentHealth < _maxHealth)
        {
            CurrentHealth++;
        }

        Debug.Log(CurrentHealth.ToString());
    }
}
