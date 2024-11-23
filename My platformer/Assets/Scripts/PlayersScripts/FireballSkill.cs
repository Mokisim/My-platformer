using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSkill : PlayerSkills
{
    public event Action FireballThrowed;

    [SerializeField] private Fireball _fireballPrefab;
    [SerializeField] private PlayerAttacker _attacker;
    [SerializeField]private Transform _attackPoint;
    [SerializeField]private ObjectPool _pool;


    private void Awake()
    {
        _pool = FindObjectOfType<ObjectPool>();
    }

    public override bool SetInput()
    {
        return InputReader.GetFireballInput();
    }

    public override void UseSkill()
    {
        ThrowFireball();
    }

    private void ThrowFireball()
    {
        if(_attacker.AttackCount >= 3)
        {
            Vector3 spawnPoint = new Vector3(transform.position.x, _attackPoint.position.y, transform.position.z);

            var fireBall = _pool.GetObject();

            fireBall.gameObject.SetActive(true);
            fireBall.transform.position = spawnPoint;

            FireballThrowed.Invoke();
        }
    }
}
