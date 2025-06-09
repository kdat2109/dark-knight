using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Dat
{
    public class WaveSystem : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] enemies;
        [SerializeField]
        private Transform[] spawnPoint;
        [SerializeField]
        private float timeSpawn = 1;

        [SerializeField]
        private float timeEndWave;

        private void Start()
        {
            StartCoroutine(EndWave());
            StartCoroutine(SpawnEnemies());
        }

        IEnumerator EndWave()
        {
            yield return new WaitForSeconds(timeEndWave);
            StopAllCoroutines();
        }

        IEnumerator SpawnEnemies()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeSpawn);    
                int indexEnemy = Random.Range(0, enemies.Length);
                int indexPoint = Random.Range(0, spawnPoint.Length);
                Instantiate(enemies[indexEnemy], spawnPoint[indexPoint].position, Quaternion.identity);
            }
        }
    }
}