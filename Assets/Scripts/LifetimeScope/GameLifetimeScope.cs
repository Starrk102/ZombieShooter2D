using Ammo;
using Bullet;
using Enemy;
using Health;
using Manager;
using Player;
using ScriptableObject;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace LifetimeScope
{
    public class GameLifetimeScope : VContainer.Unity.LifetimeScope
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Vector2 playerSpawnPosition;
        [SerializeField] private GameObjectCatalog gameObjectCatalog;
        [SerializeField] private EnemySpawn enemySpawn;
        [SerializeField] private Transform[] spawnPointsEnemy;
        [SerializeField] private AmmoUI ammoUI;
        [SerializeField] private GameOverScreen gameOverUI;
        [SerializeField] private Canvas canvas;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(gameObjectCatalog).AsSelf();
            builder.RegisterComponent(spawnPointsEnemy);

            InitUI(builder);
            InitPlayer(builder);
            InitEnemy(builder);
            InitGameOver(builder);

            builder.RegisterEntryPoint<GameManager>();
        }

        private void InitPlayer(IContainerBuilder builder)
        {
            var playerController = Instantiate<PlayerController>(playerPrefab.GetComponent<PlayerController>(),
                playerSpawnPosition, new Quaternion());
            builder.RegisterComponent<PlayerController>(playerController);
            builder.RegisterComponent<BulletPool>(playerController.GetComponent<BulletPool>());
            builder.RegisterComponent<IHealth>(playerController.GetComponent<Health.Health>());
            builder.RegisterComponent<IAmmoManager>(playerController.GetComponent<AmmoManager>());
        }

        private void InitEnemy(IContainerBuilder builder)
        {
            var spawner = Instantiate<EnemySpawn>(this.enemySpawn);
            builder.RegisterComponent<EnemySpawn>(spawner);
            builder.RegisterComponent<IEnemyFactory>(spawner.GetComponent<EnemyFactory>());
        }

        private void InitUI(IContainerBuilder builder)
        {
            var UI = Instantiate<AmmoUI>(ammoUI, canvas.transform);
            builder.RegisterComponent<AmmoUI>(UI);
        }

        private void InitGameOver(IContainerBuilder builder)
        {
            var ui = Instantiate<GameOverScreen>(gameOverUI, canvas.transform);
            ui.gameObject.SetActive(false);
            builder.RegisterComponent<GameOverScreen>(ui);
            builder.RegisterEntryPoint<GameOverScreenPresenter>();
        }
    }
}