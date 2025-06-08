using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        max.Copy(data);
    }

    [SerializeField]
    private bool canShowInfo;

    [SerializeField]
    private bool isAi;

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
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("lose");
            }
        }
    }
}
