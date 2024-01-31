using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private AudioClip _itemClip;

    public void DestroyWithSound()
    {
        PlaySound();
        Destroy(gameObject);
    }

    private void PlaySound()
    {
        AudioSource.PlayClipAtPoint(_itemClip, transform.position);
    }

}
