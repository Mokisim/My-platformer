using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _objectCount;

    private List<Transform> _points;

    private void Awake()
    {
        _points = new List<Transform>(_spawnPoint.childCount);

        for (int i = 0; i < _spawnPoint.childCount; i++)
        {
            _points.Add(_spawnPoint.GetChild(i));
        }

        if (_objectCount <= 0)
        {
            Spawn(_points);
        }
        else
        {
            Spawn(GenerateRandomSpawnPoints());
        }
    }

    private void Spawn(List<Transform> points)
    {
        if (_spawnPoint.childCount > 0)
        {
            for (int i = 0; i < points.Count; i++)
            {
                _objectPrefab = Instantiate(_objectPrefab, points[i].position, Quaternion.identity);
            }
        }
        else
        {
            _objectPrefab = Instantiate(_objectPrefab, _spawnPoint.position, Quaternion.identity);
        }
    }

    private List<Transform> GenerateRandomSpawnPoints()
    {
        List<Transform> randomIndices = new List<Transform>(_points);
        List<Transform> randomPoints = new List<Transform>(_objectCount);
        int minRandomNumber = 0;

        while (randomPoints.Count < _objectCount)
        {
            int randomIndex = Random.Range(minRandomNumber, randomIndices.Count);

            randomPoints.Add(randomIndices[randomIndex]);
            randomIndices.RemoveAt(randomIndex);
        }

        return randomPoints;
    }
}
