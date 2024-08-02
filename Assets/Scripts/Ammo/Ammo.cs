using Player;
using UnityEngine;

namespace Ammo
{
    public class Ammo : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                IAmmoManager ammoManager = other.GetComponent<AmmoManager>();
                if (ammoManager != null)
                {
                    ammoManager.AddAmmo(Random.Range(0, 3));
                    Destroy(this.gameObject); 
                }
            }
        }
    }
}