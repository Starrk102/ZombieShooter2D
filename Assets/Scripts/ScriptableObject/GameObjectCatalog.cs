using UnityEngine;

namespace ScriptableObject
{
    [CreateAssetMenu(fileName = "GameObjectCatalog", menuName = "Scriptable Objects/GameObjectCatalog")]
    public class GameObjectCatalog : UnityEngine.ScriptableObject
    {
        public GameObject bullet;
        public GameObject enemy;
        public GameObject ammo;
    }
}
