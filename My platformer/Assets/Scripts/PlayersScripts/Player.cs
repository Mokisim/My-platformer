using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const string _respawnHash = "Respawn";

    public void TakeDamage()
    {
        GameObject playerSpawn = GameObject.FindWithTag(_respawnHash);

        transform.position = playerSpawn.transform.position;
    }
}
