using System;
using System.Collections;
using System.Collections.Generic;
using _Dat;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Serializable]
    public class Data
    {
        public float health;
        public float damage;
        public float speed;
        public float attackSpeed;
        

        public void Copy(Data data)
        {
            health = data.health;
            damage = data.damage;
            speed = data.speed;
            attackSpeed = data.attackSpeed;
        }
    }
    [HideInInspector]
    public Data max;
    private Data initData = new Data();
    public Data data;
    [SerializeField]
    private bool canShowInfo;

    [SerializeField]
    private bool isAi;
    [SerializeField]
    private GameObject vfx;

    public int gold;
    
    private void Awake()
    {
        max.Copy(data);
        initData.Copy(data);
    }

    public void Clear()
    {
        data.Copy(initData);
    }

    public void AddStats(Data addData)
    {
        data.health += addData.health;
        data.damage += addData.damage;
        data.speed += addData.speed;
        data.attackSpeed += addData.attackSpeed;
        
        max.health += addData.health;
        max.damage += addData.damage;
        max.speed += addData.speed;
        max.attackSpeed += addData.attackSpeed;
    }
    
    
    public void AddHealth(float health)
    {
        data.health += health;
        data.health = Mathf.Clamp(data.health, 0, max.health);
        if(canShowInfo)
        {
            if (isAi)
            {
                GameManager.Instance.ShowDamagePopup($"{health}", Color.white, transform.position);
                if (vfx)
                {
                    var cloneVfx = Instantiate(vfx, transform.position, Quaternion.identity);
                    Destroy(cloneVfx,2);
                }
            }
            else
            {
                if (health > 0)
                {
                    GameManager.Instance.ShowDamagePopup($"+{health}", Color.green, transform.position);
                }
                else
                {
                    GameManager.Instance.ShowDamagePopup($"{health}", Color.red, transform.position);
                }
            }

        }

        if (data.health <= 0)
        {
            if (isAi)
            {
                Die();
            }
            else
            {
                Debug.Log("lose");
                PlayerDead();
            }
        }
    }
    
    public void PlayerDead()
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }
        GameManager.Instance.IsGameOver = true;
        UIManager.Instance.ShowLosePanel();
        GetComponent<PlayerController>().isDead = true;
        GetComponent<WeaponController>().isDead = true;
    }
    public void Die()
    {
        UIManager.Instance.AddGold(gold);
        GameManager.Instance.ShowDamagePopup($"{gold}", Color.yellow, transform.position + Vector3.up);
        Destroy(gameObject);
        
    }
}
