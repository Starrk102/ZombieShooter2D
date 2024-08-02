using ScriptableObject;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Enemy
{
    public class EnemyFactory : MonoBehaviour, IEnemyFactory
    {
        [Inject]private IObjectResolver objectResolve;
        [Inject]private GameObjectCatalog gameObjectCatalog;
        
        public GameObject CreateEnemy(Vector3 position)
        {
            GameObject enemy = objectResolve.Instantiate(gameObjectCatalog.enemy, position, Quaternion.identity);
            return enemy;
        }
    }
}