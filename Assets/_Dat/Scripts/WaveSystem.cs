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

       [SerializeField]
       int maxHealth;
       int currentHealth;
       [SerializeField]
       private Shop shop;
       

        private void Start()
        {
            StartWave();
            
        }


        void StartWave()
        {
            currentHealth = maxHealth;
            
            Debug.Log("▶️ Bắt đầu Wave " + currentWave);
            
            timePlay = 0;
            waveIsRunning = true;
            waveIsStopped = false;
            
            if (currentWave >= waves.Length)
            {
                Debug.Log("Hoàn thành tất cả các wave");
            }

            UIManager.Instance.gameplayUI.SetWave(currentWave + 1);

            StartCoroutine(SpawnEnemies(waves[currentWave]));
            
            shop.RollItems();
            
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


        private float timePlay;
        
        void Update()
        {
            // if (timePlay >= timeEndWave)
            // {
            //     UIManager.Instance.gameplayUI.SetTime(0);
            //     StopAllCoroutines();
            //     
            // }
            // else
            // {
            //     timePlay+= Time.deltaTime;
            //     UIManager.Instance.gameplayUI.SetTime(timeEndWave-timePlay);
            // }
            

            if (waveIsRunning)
            {
                timePlay += Time.deltaTime;
                UIManager.Instance.gameplayUI.SetTime(timeEndWave-timePlay);

                if (timePlay >= timeEndWave)
                {
                    EndWave();
                    shop.ShowShop();
                    
                }
            }
        }

        public void NextWave()
        {
            if (waveIsStopped)
            {
                StartWave();
            }
           
        }

        public void EndWave()
        {
            waveIsRunning = false;
            waveIsStopped = true;

            KillAllEnemies();
            StopAllCoroutines();
            
            currentWave++;
        }

        void KillAllEnemies()
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (var enemy in enemies)
            {
                enemy.Die();
            }
        }
        
        private bool waveIsRunning = false;
        private bool waveIsStopped = false;
        
        
        
        
        
        //todo làm hết wave thì dừng lại, quái chết hết, ấn space thì mới next wave( chỉ khi hết thời gian mới ấn đc)
        
        
        
        /*IEnumerator EndWave()
        {
            // yield return new WaitForSeconds(timeEndWave);
            for (int i = 0; i < timeEndWave; i++)
            {
                UIManager.Instance.gameplayUI.SetTime(timeEndWave-i);
                yield return new WaitForSeconds(1);
            }
            UIManager.Instance.gameplayUI.SetTime(0);

            StopAllCoroutines();
        }*/

    }
    [Serializable]
    public class DataWave
    {
        public GameObject[] enemies;
        public int min;
        public int max;
    }
    
    
}