using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private ObjectPool _pool;

    private void Start()
    {
        if (_pool == null)
        {
            _pool = FindObjectOfType<ObjectPool>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Fireball fireball))
        {
            _pool.PutObject(fireball.transform);
        }
    }
}
