using UnityEngine;

namespace _Dat
{
    public class Weapon : EquipItem
    {
        public float damage;
        public float range;
        public float fireRate;
        public virtual void Attack(Stats.Data stats)
        {
            float totalDamage = stats.damage + damage;
            Debug.Log(totalDamage);
        }
    }
    
}