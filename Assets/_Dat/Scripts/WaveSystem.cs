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
        
        public int currentWave ;
       [SerializeField]
       private Shop shop;

       private GameObject currentBoss;
       
       public void ResetWave()
       {
           waveIsRunning = false;
           currentWave = 0;
           timePlay = 0;
           GameManager.Instance.SaveData(0,new List<string>());
       }
       public void SetWave(int wave)
       {
           waveIsRunning = false;
           currentWave = wave;
           timePlay = 0;
       }
        public void StartWave()
        {
            Debug.Log("Bat dau Wave " + currentWave);
            
            timePlay = 0;
            waveIsRunning = true;
            
            if (currentWave >= waves.Length)
            {
                Debug.Log("xong het wave");
                UIManager.Instance.ShowWinPanel();
                return;
            }

            UIManager.Instance.gameplayUI.SetWave(currentWave + 1);

            StartCoroutine(SpawnEnemies(waves[currentWave]));
            
            shop.RollItems();
            
            
        }
        
        IEnumerator SpawnEnemies(DataWave wave)
        {
            if (wave.boss)
            {
                int indexPoint = Random.Range(0, spawnPoint.Length);
                Vector2 baseSpawnPos = spawnPoint[indexPoint].position;
                currentBoss = Instantiate(wave.boss, baseSpawnPos, Quaternion.identity);
            }
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
            if (waveIsRunning)
            {
                timePlay += Time.deltaTime;
                
                float reamainingTime = timeEndWave - timePlay;
                if (reamainingTime <= 0 && currentBoss != null)
                {
                    reamainingTime = 0;
                }
                UIManager.Instance.gameplayUI.SetTime(reamainingTime);

                if (timePlay >= timeEndWave && currentBoss == null)
                {
                    var allItem = shop.SaveEquipment();
                    EndWave();
                    GameManager.Instance.SaveData(currentWave,allItem);
                    shop.ShowShop();
                    shop.RollItems();
                    GameManager.Instance.IsGamePaused = true;
                }

                
            }
        }

        public void ForceEndWave()
        {
            waveIsRunning = false;
            KillAllEnemies();
            StopAllCoroutines();
            shop.ShowShop();
            shop.RollItems();
            GameManager.Instance.IsGamePaused = true;
        }

        public void NextWave()
        {
            if (!waveIsRunning)
            {
                StartWave();
            }
           
        }

        public void EndWave()
        {
            waveIsRunning = false;

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
            
            Bullet[] bullets = FindObjectsOfType<Bullet>();
            foreach (var bullet in bullets)
            {
                Destroy(bullet.gameObject);
            }
        }
        
        private bool waveIsRunning = false;

    }
    [Serializable]
    public class DataWave
    {
        public GameObject boss;
        public GameObject[] enemies;
        public int min;
        public int max;
    }
    
    
}