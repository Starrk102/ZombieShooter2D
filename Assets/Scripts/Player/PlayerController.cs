using System;
using Ammo;
using Bullet;
using Health;
using UnityEngine;
using VContainer;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public Transform firePoint;
        public float speed = 5f;
        public float fireRate = 0.1f;
        
        public Transform arm;
        public float maxArmAngle = 95f;
        
        [Inject] private BulletPool bulletPool;
        
        private Rigidbody2D rb;
        private bool facingRight = true;
        private float nextFire = 0.5f;
        
        private Camera camera1;
        private Animator animator;
        private static readonly int Speed = Animator.StringToHash("Speed");

        private IHealth health;
        private IAmmoManager ammoManager;
        
        private void Awake()
        {
            camera1 = Camera.main;
            animator = GetComponent<Animator>();
            health = GetComponent<IHealth>();
            ammoManager = GetComponent<IAmmoManager>();
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            ammoManager.InitializeAmmo();
            health.InitializeHealth();
        }

        private void Update()
        {
            Move();
            Shoot();
            RotateArm();
        }
        
        private void Move()
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            
            animator.SetFloat(Speed, Mathf.Abs(moveInput));
            
            if (moveInput > 0 && !facingRight)
            {
                Flip();
            }
            else if (moveInput < 0 && facingRight)
            {
                Flip();
            }
        }

        private void Shoot()
        {
            if (!ammoManager.HasAmmo())
                return;
            
            if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                FireBullet();
                ammoManager.UseAmmo(1);
            }
        }
        
        private void RotateArm()
        {
            Vector2 mousePosition = camera1.ScreenToWorldPoint(Input.mousePosition);
            
            if ((facingRight && mousePosition.x < transform.position.x) || (!facingRight && mousePosition.x > transform.position.x))
            {
                Flip();
            }
        }
        
        private void FireBullet()
        {
            GameObject bullet = bulletPool.Get();
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            Vector2 mousePosition = camera1.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2)firePoint.position).normalized;

            bullet.GetComponent<Bullet.Bullet>().Initialize(direction);
        }

        private void Flip()
        {
            facingRight = !facingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }
}
