using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    private Health _player;
    private float _damage = 1;
    
    private void Awake()
    {
        _player = FindObjectOfType<Health>();
    }

    public void Play(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Damage()
    {
        _player.TakeDamage(_damage);
    }

    public void Heal()
    {
        _player.RestoreHealth(_damage);
    }
}
