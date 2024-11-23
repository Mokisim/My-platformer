using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _prefab;

    private Queue<Transform> _pool;

    public IEnumerable<Component> PooledObjects => _pool;

    private void Awake()
    {
        _pool = new Queue<Transform>();

        if(this.gameObject.TryGetComponent(out Player player))
        {
            _container = FindObjectOfType<PlayerProjectilesContainer>().transform;
        }
    }

    public Component GetObject()
    {
        if (_pool.Count == 0)
        {
            var component = Instantiate(_prefab, Vector3.zero, transform.rotation);
            component.transform.parent = _container;

            return component;
        }

        return _pool.Dequeue();
    }

    public void PutObject(Transform component)
    {
        _pool.Enqueue(component);
        component.gameObject.SetActive(false);
    }

    public void Reset()
    {
        _pool.Clear();

        for (int i = 0; i < _container.childCount; i++)
        {
            Destroy(_container.GetChild(i).gameObject);
        }
    }
}
