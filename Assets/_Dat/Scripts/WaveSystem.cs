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
        private Transform[] spawnPoint;
        [SerializeField]
        private float timeSpawn = 1;

        [SerializeField]
        private float timeEndWave;
        
        public DataWave[] waves;
        
        int currentWave = 0;

        private void Start()
        {
            StartCoroutine(EndWave());
            StartCoroutine(SpawnEnemies(waves[currentWave]));
        }



        /*IEnumerator SpawnEnemies(DataWave wave)
        {
            while (true)
            {
                yield return new WaitForSeconds(timeSpawn);    
                
                int indexEnemy = Random.Range(0, wave.enemies.Length);
                GameObject enemy = wave.enemies[indexEnemy];
                
                int randomCount = Random.Range(wave.min, wave.max);
                
                for (int i = 0; i < randomCount; i++)
                {
                    int indexPoint = Random.Range(0, spawnPoint.Length);

                    float randomX = Random.Range(-2, 2);
                    float randomY = Random.Range(-2, 2);
                    
                    Vector2 spawnPos = (Vector2)spawnPoint[indexPoint].position + new Vector2(randomX, randomY);
                    
                    Instantiate(enemy, spawnPos, Quaternion.identity);
                }
            }
        }*/
        
        IEnumerator SpawnEnemies(DataWave wave)
        {
            while (true)
            {
                yield return new WaitForSeconds(timeSpawn);    

                int randomCount = Random.Range(wave.min, wave.max); // Số lượng quái sẽ sinh

                // Chọn 1 spawn point duy nhất cho lần này
                int indexPoint = Random.Range(0, spawnPoint.Length);
                Vector2 baseSpawnPos = spawnPoint[indexPoint].position;

                for (int i = 0; i < randomCount; i++)
                {
                    // Chọn loại quái ngẫu nhiên cho mỗi lần sinh
                    int indexEnemy = Random.Range(0, wave.enemies.Length);
                    GameObject enemy = wave.enemies[indexEnemy];

                    // Tạo thêm vị trí ngẫu nhiên nhỏ xung quanh spawnPoint
                    float randomX = Random.Range(-2f, 2f);
                    float randomY = Random.Range(-2f, 2f);
                    Vector2 spawnPos = baseSpawnPos + new Vector2(randomX, randomY);

                    Instantiate(enemy, spawnPos, Quaternion.identity);
                }
            }
        }

        
        
        
        
        
        IEnumerator EndWave()
        {
            yield return new WaitForSeconds(timeEndWave);
            StopAllCoroutines();
        }

    }
    [Serializable]
    public class DataWave
    {
        public GameObject[] enemies;
        public int min;
        public int max;
    }
}