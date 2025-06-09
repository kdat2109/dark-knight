using UnityEngine;

namespace _Dat
{
    public class Bullet : MonoBehaviour
    {
        public string enemyTag;
        public float damage;
        public float range;
        private Vector2 startPos;

        void Start()
        {
            startPos = transform.position;
        }

        void Update()
        {
            // Tự hủy nếu đi quá tầm
            if (Vector2.Distance(startPos, transform.position) > range)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Va chạm với enemy
            if (collision.CompareTag(enemyTag))
            {
                Stats stats = collision.GetComponent<Stats>();
                stats.AddHealth(-damage);
                // Gây sát thương tại đây nếu cần
                Debug.Log($"Đã gây {damage} lên {collision.name}");
                Destroy(gameObject);
            }
        }
    }
}