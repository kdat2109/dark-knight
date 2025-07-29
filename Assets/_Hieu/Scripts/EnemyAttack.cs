using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Stats player;
    [SerializeField]
    private float attackDistance;

    [SerializeField]
    private Stats enemyStats;

    private float timeAttack;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
    }

    void Update()
    {
        if (player)
        {
            var distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < attackDistance && timeAttack < Time.time)
            {
                timeAttack = Time.time + enemyStats.data.attackSpeed;
                player.AddHealth(-enemyStats.data.damage);
            }
        }
    }
}