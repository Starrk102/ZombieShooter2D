using System;
using Player;
using TMPro;
using UnityEngine;
using VContainer;

namespace Ammo
{
    public class AmmoUI : MonoBehaviour
    {
        [Inject] private IAmmoManager ammoManager;
        
        private TMP_Text Text => this.gameObject.GetComponent<TMP_Text>();
        
        private void Start()
        {
            ammoManager.OnAmmoChanged += OnAmmoManagerOnOnAmmoChanged;
        }

        private void OnDestroy()
        {
            ammoManager.OnAmmoChanged -= OnAmmoManagerOnOnAmmoChanged;
        }
        
        private void OnAmmoManagerOnOnAmmoChanged(int value)
        {
            Text.text = value.ToString();
        }
    }
}
