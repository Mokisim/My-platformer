using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Gem : MonoBehaviour
{
    [SerializeField] private AudioClip _gemClip;

    private Collider2D _gemCollider;

    private void Awake()
    {
        _gemCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            PlaySound();
            DestroyGem();
        }
    }

    private void PlaySound()
    {
        AudioSource.PlayClipAtPoint(_gemClip, transform.position);
    }

    private void DestroyGem()
    {
        Destroy(_gemCollider.gameObject);
    }
}
