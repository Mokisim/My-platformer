using UnityEngine;

public class Player : MonoBehaviour
{
    private const string RespawnHash = "Respawn";

    [SerializeField] private int _maxHealth;
    public int CurrentHealth { get; private set; }

    private void Start()
    {
        CurrentHealth = _maxHealth;
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

        GameObject playerSpawn = GameObject.FindWithTag(RespawnHash);

        transform.position = playerSpawn.transform.position;
        Debug.Log(CurrentHealth.ToString());

        if (CurrentHealth <= 0)
        {
            Debug.Log("YOU LOST");
        }
    }

    public void RestoreHealth()
    {
        if (CurrentHealth < _maxHealth)
        {
            CurrentHealth++;
        }
        else
        {
            return;
        }

        Debug.Log(CurrentHealth.ToString());
    }
}
