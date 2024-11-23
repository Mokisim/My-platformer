using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Fireball : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private int _damage = 1;
    private Rigidbody2D _fireball;
    
    private void Awake()
    {
        _fireball = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _fireball.velocity = transform.right * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != gameObject)
        {
            if (collision.TryGetComponent(out Enemy enemy) == true)
            {
                enemy.GetComponent<Health>()?.TakeDamage(_damage);
            }
        }
    }
}
