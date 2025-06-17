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
    public Data data;
    [SerializeField]
    private bool canShowInfo;

    [SerializeField]
    private bool isAi;
    
    private void Start()
    {
        max.Copy(data);
        
    }


    
    
    public void AddHealth(float health)
    {
        data.health += health;
        if(canShowInfo)
        {
            if (isAi)
            {
                GameManager.Instance.ShowDamagePopup($"{health}", Color.white, transform.position);
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
        GameManager.Instance.IsGameOver = true;
        UIManager.Instance.ShowLosePanel();
        GetComponent<PlayerController>().isDead = true;
        GetComponent<WeaponController>().isDead = true;
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
