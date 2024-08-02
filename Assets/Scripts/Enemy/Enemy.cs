using System;
using Health;
using Player;
using ScriptableObject;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        public float speed = 3f;
        public int damage = 999;
        [Inject] private PlayerController playerController;
        [Inject] private GameObjectCatalog gameObjectCatalog;
        [Inject] private IObjectResolver objectResolver;
        private Transform target;
        private Rigidbody2D rb;
        private Vector3 originalScale;
        
        private IHealth health;
        
        private void Awake()
        {
            health = GetComponent<IHealth>();
            health.InitializeHealth();
        }
        
        private void Start()
        {
            target = playerController.transform;
            rb = GetComponent<Rigidbody2D>();
            originalScale = transform.localScale;

            health.OnDeath += () =>
            {
                Destroy(this.gameObject);
            };
        }

        private void Update()
        {
            if (target != null)
            {
                Vector2 direction = (target.position - transform.position).normalized;
                rb.velocity = direction * (speed *  Random.Range(1, 2));
                
                if (direction.x > 0)
                {
                    transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
                }
            }
        }

        private void OnDestroy()
        {
            DropAmmo();
        }
        
        private void DropAmmo()
        {
            objectResolver.Instantiate(gameObjectCatalog.ammo, transform.position, Quaternion.identity);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<IHealth>().TakeDamage(damage);
            }
        }
    }
}