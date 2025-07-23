using System;
using UnityEngine;

namespace _Dat
{
    public class Sword : Weapon
    {
        float totalDamage;
        [SerializeField]
        Animator animator;
        public AudioClip audioCol,audioAttack;

        private float timeAttack;
        public override void Attack(Stats.Data stats)
        {
            if (timeAttack < Time.time)
            {
                totalDamage = stats.damage + damage;
                animator.SetTrigger("Attack");
                timeAttack = Time.time + fireRate - stats.attackSpeed;
                SoundManager.PlaySound(audioAttack);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))// Chạm vào enemy
            {
                Stats stats = other.GetComponent<Stats>();
                stats.AddHealth(-totalDamage);
                SoundManager.PlaySound(audioCol);
            }
        }
    }
}