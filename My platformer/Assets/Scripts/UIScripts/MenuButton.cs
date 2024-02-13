using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    private const string PlayerHash = "Player";

    private GameObject _player;
    private int _delay = 1;

    private void Start()
    {
        Invoke("FindPlayer", _delay);
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
        _player.GetComponent<PlayerHealth>().TakeDamage();
    }

    public void Heal()
    {
        _player.GetComponent<PlayerHealth>().RestoreHealth();
    }

    private void FindPlayer()
    {
        _player = GameObject.FindWithTag(PlayerHash);
    }
}
