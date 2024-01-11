using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] private AudioClip _gemClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            PlaySound();
            Destroy();
        }
    }

    private void PlaySound()
    {
        AudioSource.PlayClipAtPoint(_gemClip, transform.position);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
