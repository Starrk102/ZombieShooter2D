using System;

namespace Ammo
{
    public interface IAmmoManager
    {
        int CurrentAmmo { get; }
        void AddAmmo(int amount);
        void UseAmmo(int amount);
        bool HasAmmo();
        void InitializeAmmo();
        event Action<int> OnAmmoChanged;
    }
}