using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private const string RespawnHash = "Respawn";

    public float CurrentHealth { get; private set; }
    public float MaxHealth {  get; private set; }

    private GameObject _playerSpawn;
    private float _maxHealth = 3;

    private void Awake()
    {
        MaxHealth = _maxHealth;
        CurrentHealth = MaxHealth;
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
    }

    public void RestoreHealth()
    {
        if (CurrentHealth < MaxHealth)
        {
            CurrentHealth++;
        }

        Debug.Log(CurrentHealth.ToString());
    }
}
