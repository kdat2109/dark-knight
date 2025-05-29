using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform target; // Gán Player từ Editor hoặc tự tìm trong Start

    private Rigidbody2D rb;
    private Vector2 movement;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Nếu chưa gán target, tự động tìm theo tag "Player"
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        // Tính hướng di chuyển
        Vector2 direction = (target.position - transform.position).normalized;
        movement = direction;

        // Di chuyển enemy
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
        
        animator.SetFloat("RunState", movement.magnitude);

        FlipCharacter();
    }
    
    void FlipCharacter()
    {
        if (movement.x > 0)
        {
            animator.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (movement.x < 0)
        {
            animator.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("take damage");
        }
    }
}
