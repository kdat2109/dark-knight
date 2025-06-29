using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Dat
{
    public class WeaponController : MonoBehaviour
    {
        //sau
        public List<Weapon> weapons = new List<Weapon>();
        public List<Transform> slots = new List<Transform>();
        public Stats stats;
        public bool isDead;

        // thêm vũ khí vào slots 
        public void Equip(Weapon weapon)
        {
            if (weapons.Count >= slots.Count)
            {
                Debug.Log("Đã đủ vũ khí !");
                return;
            }

            int indexSlot = weapons.Count;
            var wp = Instantiate(weapon,slots[indexSlot].position , Quaternion.identity);
            wp.transform.parent = slots[indexSlot];
            
            weapons.Add(wp);
        }

        
        private void Update()
        {
            if (isDead) return;
            if (EnemyManager.Enemies.Count == 0) return; // nếu enemy không có thì return
            foreach (Weapon weapon in weapons)
            {
                if (weapon.isAttacking)
                {
                    continue;
                }
                Transform nearestEnemy = GetNearestEnemy(weapon.transform);
                if (nearestEnemy == null) continue;

                Vector2 direction = nearestEnemy.position - weapon.transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                weapon.transform.rotation = Quaternion.Euler(0, 0, angle - 90);


                if (direction.magnitude <= weapon.range)
                {
                    weapon.Attack(stats.data);
                    weapon.SetDelayAttack();
                }
            }
        }

        
        private Transform GetNearestEnemy(Transform weaponTransform)
        {
            Transform nearest = null;
            float minDistance = Mathf.Infinity;

            foreach (Transform enemy in EnemyManager.Enemies)
            {
                float dist = Vector3.Distance(weaponTransform.position, enemy.position);
                if (dist<minDistance)
                {
                    minDistance = dist;
                    nearest = enemy;
                }
            }
            return nearest;
        }
    }
}