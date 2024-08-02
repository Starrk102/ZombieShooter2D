using System;
using Health;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;

namespace Bullet
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 10f;
        public int damage = 999;
        private Renderer rend;
        private Rigidbody2D rb;
        [Inject] private BulletPool pool;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            rend = GetComponent<Renderer>();
        }

        private void Update()
        {
            if (!rend.isVisible)
            {
                ReturnToPool();
            }
            
            transform.Translate(rb.velocity * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<IHealth>().TakeDamage(damage);
                ReturnToPool();
            }
        }
        
        public void Initialize(Vector2 direction)
        {
            rb.velocity = direction * speed;
        }
        
        private void ReturnToPool()
        {
            rb.velocity = Vector2.zero;
            pool.ReturnToPool(gameObject);
        }
    }
}