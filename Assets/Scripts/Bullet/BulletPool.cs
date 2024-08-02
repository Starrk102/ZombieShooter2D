using System.Collections.Generic;
using ScriptableObject;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Bullet
{
    public class BulletPool : MonoBehaviour
    {
        [Inject]private IObjectResolver objectResolver;
        [Inject]private GameObjectCatalog gameObjectCatalog;
        private GameObject bullet;
        
        private int initialSize = 10;
        
        private Queue<GameObject> poolQueue = new Queue<GameObject>();
        
        public void Start()
        {
            bullet = this.gameObjectCatalog.bullet;
            
            for (int i = 0; i < initialSize; i++)
            {
                var obj = this.objectResolver.Instantiate(bullet);
                obj.SetActive(false);
                poolQueue.Enqueue(obj);
            }
        }

        public GameObject Get()
        {
            if (poolQueue.Count == 0)
            {
                var obj = objectResolver.Instantiate(bullet);
                obj.SetActive(false);
                poolQueue.Enqueue(obj);
            }

            GameObject pooledObject = poolQueue.Dequeue();
            pooledObject.SetActive(true);
            return pooledObject;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }
}