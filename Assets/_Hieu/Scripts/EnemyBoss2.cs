using System;
using System.Collections;
using System.Collections.Generic;
using _Dat;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBoss2 : MonoBehaviour
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
            if (GetComponent<Stats>().data.health < 100)
            {
                StartCoroutine(SpamSingleBullet(10,0.2f));
            }
            else
            {
                StartCoroutine(SpamSingleBullet(3,0.2f));
            }

        }
        else
        {

            StartCoroutine(SpamBullet(5,10, 0.2f));

        }

    }

    IEnumerator SpamSingleBullet(int bulletCount ,float delayPer)
    {
        GetComponent<EnemyMovement>().canMove = false;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = 360f/bulletCount * i;
            var bl = Instantiate(bullet, transform.position, Quaternion.identity);
            var rb = bl.GetComponent<Rigidbody2D>();
            Vector2 direction = (player.transform.position - transform.position).normalized;
            var rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg ;
            bl.transform.rotation = Quaternion.Euler(0,0,rotZ+ angle);
            rb.velocity = direction * bulletSpeed;
            yield return new WaitForSeconds(delayPer);
        }
        GetComponent<EnemyMovement>().canMove = true;
        
    }
    IEnumerator SpamBullet(int wave,int bulletCount,float delayPer)
    {
        GetComponent<EnemyMovement>().canMove = false;
        for (int w = 0; w < wave; w++)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = 360f/bulletCount * i;
                var bl = Instantiate(multiBullet, transform.position, Quaternion.identity);
                var rb = bl.GetComponent<Rigidbody2D>();
                rb.GetComponent<BulletDelay>().delay -= 0.1f * w;
                Vector2 direction = (player.transform.position - transform.position).normalized;
                var rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg ;
                bl.transform.rotation = Quaternion.Euler(0,0,rotZ+ angle + w * (360f / bulletCount / 2f));
                rb.velocity = bl.transform.right * (bulletSpeed);
            }
            yield return new WaitForSeconds(delayPer);
        }

        GetComponent<EnemyMovement>().canMove = true;
    }
}
