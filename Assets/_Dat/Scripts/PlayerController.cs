    using System.Collections;
    using System.Collections.Generic;
    using _Dat;
    using UnityEngine;

    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private Rigidbody2D rb;
        private Vector2 moveInput;

        public Stats stats;
        public bool isDead = false;
        Vector3 startPos;
        

        void Start()
        {
            startPos = transform.position;
            rb = GetComponent<Rigidbody2D>();
            InitPlayer();
        }

        public void ResetPos()
        {
            transform.position = startPos;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                    rb.velocity = Vector2.zero;
                    rb.angularVelocity = 0f;
            }
        }

        public void InitPlayer()
        {
            GetComponent<WeaponController>().Clear();
            GetComponent<Equipment>().ClearAndReset();
            stats.Clear();
            isDead = false;
        }

        void Update()
        {
            if (GameManager.Instance.IsGamePaused || isDead)
            {
                moveInput = Vector2.zero;
                return;
            }
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
