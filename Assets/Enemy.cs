using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EnemyManager.Enemies.Add(transform); // thêm vào list enemy 
    }

    private void OnDestroy()
    {
        EnemyManager.Enemies.Remove(transform); // xóa enemy khỏi list  
    }
}
