using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float moveSpeed = 3f;
    public float minRange;
    public Transform target; // Gán Player từ Editor hoặc tự tìm trong Start

    private Rigidbody2D rb;
    private Vector2 movement;
    public Animator animator;
    private Stats stats;

    public bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<Stats>();
        moveSpeed = stats.data.speed;
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
        if (target == null)
        {
            rb.velocity = Vector2.zero;
        animator.SetFloat("RunState", 0);
            return;
        }

        if (!canMove)
        {
            rb.velocity = Vector2.zero;
        animator.SetFloat("RunState", 0);
            
            return;
        }
        float distance = Vector2.Distance(transform.position, target.position);

        // Tính hướng di chuyển
        Vector2 direction = (target.position - transform.position).normalized;
        
        if (distance > minRange)
        {
            movement = direction;
            
        }
        else if(distance < minRange -1)
        {
            movement = -direction;
        }
        else
        {
            movement =Vector2.zero;
        }
        // Di chuyển
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
}
