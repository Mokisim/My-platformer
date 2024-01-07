using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    [SerializeField] private GemScript _gemPrefab;
    [SerializeField] Transform _spawnPoint;

    private Transform[] _points;

    private void Awake()
    {
        _points = new Transform[_spawnPoint.childCount];

        for (int i = 0; i < _spawnPoint.childCount; i++)
        {
            _points[i] = _spawnPoint.GetChild(i);
        }
    }

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            _gemPrefab = Instantiate(_gemPrefab, _points[i].position, Quaternion.identity);
        }
    }
}
