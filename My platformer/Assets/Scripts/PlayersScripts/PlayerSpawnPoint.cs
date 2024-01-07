using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _spawnPoint;

    public Transform SpawnPoint { get { return _spawnPoint; } private set { } }

    private void Awake()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        _playerPrefab = Instantiate(_playerPrefab, _spawnPoint.position, Quaternion.identity);
    }
}
