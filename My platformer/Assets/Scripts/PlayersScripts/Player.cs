using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    private const string _respawnHash = "Respawn";
    public int CurrentHealth { get; private set; }

    private void Start()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage()
    {
        CurrentHealth--;

        GameObject playerSpawn = GameObject.FindWithTag(_respawnHash);

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
