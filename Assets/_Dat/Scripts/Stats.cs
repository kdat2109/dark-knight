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
    }
    public Data data;

    public void AddHealth(float health)
    {
        data.health += health;
        
    }
}
