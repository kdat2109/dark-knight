using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] spamPoint;

    [SerializeField]
    private GameObject enemy;

    [Serializable]
    public class Data
    {
        public int min, max;
    }
    public Data data = new();
    public float cooldown;
    private float rate;
    void Update()
    {
        if (rate <= 0)
        {
            rate = cooldown;
            var count = Random.Range(data.min, data.max);
            for (int i = 0; i <count; i++)
            {
                var point = spamPoint[Random.Range(0, spamPoint.Length)];
                Instantiate(enemy,point.position,Quaternion.identity).SetActive(true);
            }
        }
        else
        {
            rate -= Time.deltaTime;
        }
    }
}
