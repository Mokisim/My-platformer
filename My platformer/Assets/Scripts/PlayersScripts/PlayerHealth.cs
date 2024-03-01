using UnityEngine;

[RequireComponent(typeof(Health), typeof(PlayerCollisionHandler))]
public class PlayerHealth : MonoBehaviour
{
    private const string RespawnHash = "Respawn";

    private GameObject _playerSpawn;
    private Health _health;
    private PlayerCollisionHandler _playerCollisionHandler;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _playerCollisionHandler = GetComponent<PlayerCollisionHandler>();
    }

    private void Start()
    {
        _playerSpawn = GameObject.FindWithTag(RespawnHash);
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
