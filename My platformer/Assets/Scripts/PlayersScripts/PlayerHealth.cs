using UnityEngine;

[RequireComponent(typeof(Health), typeof(PlayerCollisionHandler))]
public class PlayerHealth : MonoBehaviour
{
    private Transform _playerSpawn;
    private Health _health;
    private PlayerCollisionHandler _playerCollisionHandler;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _playerCollisionHandler = GetComponent<PlayerCollisionHandler>();
    }

    private void Start()
    {
        _playerSpawn = FindObjectOfType<PlayerSpawnPoint>().transform;
    }

    private void OnEnable()
    {
        _health.HealthDecreased += GoToRespawn;
        _playerCollisionHandler.HealthHealed += _health.RestoreHealth;
    }

    private void OnDisable()
    {
        _health.HealthDecreased -= GoToRespawn;
        _playerCollisionHandler.HealthHealed -= _health.RestoreHealth;
    }

    public void GoToRespawn()
    {
        transform.position = _playerSpawn.transform.position;
    }
}
