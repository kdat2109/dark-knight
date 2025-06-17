    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private Rigidbody2D rb;
        private Vector2 moveInput;

        public Stats stats;
        public bool isDead = false;
        

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {   
            if(isDead) return;
            // Lấy input từ bàn phím
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            // Chuẩn hóa để tốc độ không nhanh hơn khi đi chéo
            moveInput = moveInput.normalized;

            // Cập nhật trạng thái chạy
            //bool isRunning = moveInput != Vector2.zero;
            animator.SetFloat("RunState", moveInput.magnitude);
            
            // xoay nhân vật khi sang trái hoặc sang phải bằng cách đổi scale
            FlipCharacter();
        }

        void FlipCharacter()
        {
            if (moveInput.x > 0)
            {
                animator.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (moveInput.x < 0)
            {
                animator.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        void FixedUpdate()
        {
            // lấy speed từ stats để sau này muốn tăng tốc thì thay đổi ở stats
            var speed = stats.data.speed;
            // Di chuyển nhân vật
            rb.MovePosition(rb.position + moveInput * (speed * Time.fixedDeltaTime));
        }

        
        
    }
