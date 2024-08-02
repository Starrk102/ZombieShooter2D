using System.Collections;
using Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Enemy
{
    public class EnemySpawn : MonoBehaviour
    {
        [SerializeField] private float spawnInterval = 10f;
        [Inject] private Transform[] spawnPoints;
        [Inject] private PlayerController player;
        [Inject] private IObjectResolver objectResolver;
        [Inject] private IEnemyFactory enemyFactory;
        
        void Start()
        {
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnInterval);
            }
        }
        
        private void SpawnEnemy()
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            enemyFactory.CreateEnemy(spawnPoint.position);
        }
    }
}