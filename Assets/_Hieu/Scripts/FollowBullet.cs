using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBullet : MonoBehaviour
{
    [SerializeField]
    private string targetTag;

    private Transform target;
    Rigidbody2D rb;
    [SerializeField]
    private float delay;

    private float targetTime;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float nearTarget;

    [SerializeField]
    private float damage;

    private bool isFollow;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        rb = GetComponent<Rigidbody2D>();
        targetTime = Time.time + delay;
        isFollow = true;
    }

    void Update()
    {
        if (!isFollow)
        {
            return;
        }
        if (Vector3.Distance(transform.position, target.position) < nearTarget)
        {
            isFollow = false;
            Destroy(gameObject,3);
        }
        rb.velocity = transform.right * speed;
        if (Time.time > targetTime)
        {
            Vector3 direction = target.position - transform.position;

            transform.right = Vector3.Slerp(transform.right, direction, rotationSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Va chạm với enemy
        if (collision.CompareTag(targetTag))
        {
            Stats stats = collision.GetComponent<Stats>();
            stats.AddHealth(-damage);
            // Gây sát thương tại đây nếu cần
            Debug.Log($"Đã gây {damage} lên {collision.name}");
            Destroy(gameObject);
        }
    }
}
