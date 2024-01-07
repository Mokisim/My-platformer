using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy _enemyPrefab;
    [SerializeField] Transform _spawnPoint;

    private void Awake()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        _enemyPrefab = Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);
    }
}
