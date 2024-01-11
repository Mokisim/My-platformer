using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _objectPrefab;
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
}
