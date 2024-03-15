using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class VampirismSkill : PlayerSkills
{
    [SerializeField] private LayerMask _enemyLayers;
    [SerializeField] private float _skillRadius;
    [SerializeField] private float _vampirism = 0.5f;
    private float _duration = 6f;
    private float _damageDealRate = 0.5f;
    private float _cooldown = 10f;
    private float _nextActionTime;
    private Health _health;
    private WaitForSeconds _waitCooldown;
    private WaitForSeconds _damageRate;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _waitCooldown = new WaitForSeconds(_cooldown);
        _damageRate = new WaitForSeconds(_damageDealRate);
    }

    public override bool SetInput()
    {
        return InputReader.GetVampirismInput();
    }

    public override void UseSkill()
    {
        if (Time.time >= _nextActionTime)
        {
            StartCoroutine(StealHealth());
        }
    }

    private IEnumerator StealHealth()
    {
        _nextActionTime = Time.time + _cooldown;
        float vampirismValue = _vampirism;

        for (int i = 0; i < _duration; i++)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _skillRadius, _enemyLayers);
            Dictionary<Collider2D, float> distanceEnemies = new Dictionary<Collider2D, float>();
            List<float> distance = new List<float>();

            for (int j = 0; j < enemies.Length; j++)
            {
                distance.Add(Vector2.Distance(transform.position, enemies[j].transform.position));
                distanceEnemies.Add(enemies[j], distance[j]);
            }

            if (distance.Count > 0)
            {
                float minDistance = distance.Min();


                var targetEnemies = distanceEnemies.Where(enemy => enemy.Value == minDistance);


                foreach (var enemy in targetEnemies)
                {
                    if (enemy.Key.GetComponent<Health>().CurrentHealth < _vampirism)
                    {
                        vampirismValue = enemy.Key.GetComponent<Health>().CurrentHealth;
                    }

                    if (enemy.Key.GetComponent<Health>().CurrentHealth > 0)
                    {
                        enemy.Key.GetComponent<Health>().TakeDamage(vampirismValue);
                        _health.RestoreHealth(vampirismValue);
                    }
                }

                vampirismValue = _vampirism;
            }

            yield return _damageRate;
        }

        yield return _waitCooldown;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _skillRadius);
    }
}
