using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool isDead = false;
    void Start()
    {
        EnemyManager.Enemies.Add(transform); // thêm vào list enemy 
    }

    private void OnDestroy()
    {
        EnemyManager.Enemies.Remove(transform); // xóa enemy khỏi list      
    }


    public void Die()
    {
        if(isDead) return;
        isDead = true;

        Destroy(gameObject);

    }
}
