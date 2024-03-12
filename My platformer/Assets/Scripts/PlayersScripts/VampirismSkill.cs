using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VampirismSkill : PlayerSkills
{
    [SerializeField] private LayerMask _enemyLayers;
    [SerializeField] private float _skillRadius;
    private float _damage = 2f;

    public override bool SetInput()
    {
        return InputReader.GetVampirismInput();
    }

    public override void UseSkill()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _skillRadius, _enemyLayers);
        List<float> distance = new List<float>();
        
        for (int i = 0; i < enemies.Length; i++)
        {
            distance.Add(Mathf.Sqrt((Mathf.Pow(transform.position.x, 2) + Mathf.Pow(enemies[i].transform.position.x, 2))
                + (Mathf.Pow(transform.position.y, 2) + Mathf.Pow(enemies[i].transform.position.y, 2))
                + (Mathf.Pow(transform.position.z, 2) + Mathf.Pow(enemies[i].transform.position.z, 2))));
        }

        if (enemies.Length > 0)
        {
            for (int i = 0; i < distance.Count; i++)
            {
                if (distance[i] == distance.Min())
                {
                    enemies[i].GetComponent<Health>().TakeDamage(_damage);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _skillRadius);
    }
}
