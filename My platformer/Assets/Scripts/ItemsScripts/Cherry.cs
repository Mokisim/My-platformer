using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    [SerializeField] private AudioClip _cherryClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.RestoreHealth();
            PlaySound();
            Destroy();
        }
    }

    private void PlaySound()
    {
        AudioSource.PlayClipAtPoint(_cherryClip, transform.position);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
