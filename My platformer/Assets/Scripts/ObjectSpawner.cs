using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] private int _objectCount;

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
        if (_objectCount <= 0)
        {
            Spawn();
        }
        else
        {
            RandomSpawn();
        }
    }

    private void Spawn()
    {
        if (_spawnPoint.childCount > 0)
        {
            for (int i = 0; i < _points.Length; i++)
            {
                _objectPrefab = Instantiate(_objectPrefab, _points[i].position, Quaternion.identity);
            }
        }
        else
        {
            _objectPrefab = Instantiate(_objectPrefab, _spawnPoint.position, Quaternion.identity);
        }
    }

    private List<Transform> GenerateRandomSpawnPoints()
    {
        List<Transform> randomPoints = new List<Transform>();
        int minRandomNumber = 0;

        for (int i = 0; i < _objectCount; i++)
        {
            int randomIndex = Random.Range(minRandomNumber, _points.Length);
            Transform randomPoint = _points[randomIndex];
            randomPoints.Add(randomPoint);
        }

        for (int i = 0; i < randomPoints.Count; i++)
        {
            for(int j = 1; j < randomPoints.Count; j++)
            {
                if (randomPoints[i] == randomPoints[j])
                {
                    randomPoints[i] = _points[Random.Range(minRandomNumber, _points.Length)];
                    j = 1;
                }
            }
        }

        return randomPoints;
    }

    private void RandomSpawn()
    {
        List<Transform> randomPoints = GenerateRandomSpawnPoints();

        for (int i = 0; i < randomPoints.Count; i++) 
        {
            _objectPrefab = Instantiate(_objectPrefab, randomPoints[i].position, Quaternion.identity);
        }
    }
}
