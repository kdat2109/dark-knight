using System;
using UnityEngine;

namespace _Dat
{
    public class Weapon : EquipItem
    {
        public float damage;
        public float range;
        public float fireRate;
        public bool isAttacking;
        private float delay = .5f;
        private float delayRate = 0;

        public virtual void Attack(Stats.Data stats)
        {
            float totalDamage = stats.damage + damage;
            Debug.Log(totalDamage);
        }

        public void SetDelayAttack()
        {
            delayRate = delay;
            isAttacking = true;
        }
        
        private void Update()
        {
            if (isAttacking)
            {
                if (delayRate > 0)
                {
                    delayRate -= Time.deltaTime;
                }
                else
                {
                    delayRate = delay;
                    isAttacking = false;
                }
            }
        }
    }
    
}