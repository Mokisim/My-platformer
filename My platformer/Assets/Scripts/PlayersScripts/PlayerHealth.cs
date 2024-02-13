using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private const string RespawnHash = "Respawn";

    public int CurrentHealth { get; private set; }
    public int MaxHealth {  get; private set; }

    private GameObject _playerSpawn;
    private int _maxHealth = 3;

    private void Awake()
    {
        MaxHealth = _maxHealth;
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
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
