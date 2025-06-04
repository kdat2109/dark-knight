using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Dat
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField]
        private Weapon weapon;
        //sau
        public List<Weapon> weapons = new List<Weapon>();
        public List<Transform> slots = new List<Transform>();
        public Stats stats;
        private void Update()
        {
            if (EnemyManager.Enemies.Count == 0) return;
            foreach (Weapon weapon in weapons)
            {
                Transform nearestEnemy = GetNearestEnemy(weapon.transform);
                if (nearestEnemy == null) continue;

                Vector2 direction = nearestEnemy.position - weapon.transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                weapon.transform.rotation = Quaternion.Euler(0, 0, angle - 90);


                if (direction.magnitude <= weapon.range)
                {
                    weapon.Attack(stats.data);
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