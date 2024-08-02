using System;

namespace Health
{
    public interface IHealth
    {
        int CurrentHealth { get; set; }
        int MaxHealth { get; set; }
        
        void InitializeHealth();
        void TakeDamage(int damage);
        
        event Action<int> OnHealthChanged;
        event Action OnDeath;
    }
}