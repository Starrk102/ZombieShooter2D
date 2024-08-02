using System;
using UnityEngine;

namespace Ammo
{
    public class AmmoManager : MonoBehaviour, IAmmoManager
    {
        public int CurrentAmmo { get; set; }
        
        public void AddAmmo(int amount)
        {
            CurrentAmmo += amount;
            
            OnAmmoChanged?.Invoke(CurrentAmmo);
        }

        public void UseAmmo(int amount)
        {
            if (CurrentAmmo >= amount)
            {
                CurrentAmmo -= amount;
                OnAmmoChanged?.Invoke(CurrentAmmo);
            }
        }

        public bool HasAmmo()
        {
            return CurrentAmmo > 0;
        }

        public void InitializeAmmo()
        {
            CurrentAmmo = 10;
        }

        public event Action<int> OnAmmoChanged;
    }
}