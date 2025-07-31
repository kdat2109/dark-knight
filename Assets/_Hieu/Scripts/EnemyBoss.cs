using System;
using System.Collections;
using System.Collections.Generic;
using _Dat;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBoss : MonoBehaviour
{
    public GameObject bullet;
    public GameObject multiBullet;
    
    public float attackCd;
    [SerializeField]
    float bulletSpeed;
    private float attackRate;
    private Stats player;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<EnemyMovement>().animator;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        
        UIManager.Instance.gameplayUI.InitBossHealthBar(GetComponent<Stats>());
    }

    private void OnDestroy()
    {
        UIManager.Instance.gameplayUI.HideBossHealthBar();
        
        // WaveSystem waveSystem = FindObjectOfType<WaveSystem>();
        // if (waveSystem != null)
        // {
        //     waveSystem.EndWave();
        //     waveSystem.ForceEndWave();
        // }
        
    }

    public void Update()
    {
        if (attackRate <= 0)
        {
            Attack();
            attackRate = attackCd;
        }
        else
        {
            attackRate -= Time.deltaTime;
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        if (Random.Range(0, 100) > 50)
        {
            StartCoroutine(SpamSingleBullet());

        }
        else
        {
            StartCoroutine(SpamBullet(5, 0.2f));
        }

    }

    IEnumerator SpamSingleBullet()
    {
        GetComponent<EnemyMovement>().canMove = false;
        var bl = Instantiate(bullet, transform.position, Quaternion.identity);
        var rb = bl.GetComponent<Rigidbody2D>();
        Vector2 direction = (player.transform.position - transform.position).normalized;
        var rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bl.transform.rotation = Quaternion.Euler(0,0,rotZ);
        rb.velocity = direction * bulletSpeed;
            yield return new WaitForSeconds(0.5f);
        GetComponent<EnemyMovement>().canMove = true;
    }
    IEnumerator SpamBullet(int bulletCount,float delayPer)
    {
        GetComponent<EnemyMovement>().canMove = false;
        for (int i = 0; i < bulletCount; i++)
        {
            Vector2 randomPos = new Vector2(Random.Range(-1, 1f), Random.Range(-1, 1f));
            Instantiate(multiBullet, player.transform.position + (Vector3)randomPos, Quaternion.identity);
            yield return new WaitForSeconds(delayPer);
        }
        GetComponent<EnemyMovement>().canMove = true;
    }
}
