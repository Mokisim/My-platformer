using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public void TakeDamage()
    {
        GameObject playerSpawn = GameObject.FindWithTag("Respawn");

        transform.position = playerSpawn.transform.position;
    }
}
