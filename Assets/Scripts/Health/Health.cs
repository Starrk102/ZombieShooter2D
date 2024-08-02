using System;
using UnityEngine;

namespace Health
{
    public class Health : MonoBehaviour, IHealth
    {
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }

        public event Action<int> OnHealthChanged;
        public event Action OnDeath;

        public void InitializeHealth()
        {
            MaxHealth = 100;
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Die();
            }
            
            OnHealthChanged?.Invoke(CurrentHealth);
        }
        
        private void Die()
        {
            OnDeath?.Invoke();
        }
    }
}