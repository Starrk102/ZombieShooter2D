using Health;
using Player;
using UI;
using UnityEngine;
using VContainer.Unity;

namespace Manager
{
    public class GameManager : IInitializable
    {
        private IHealth health;
        private GameOverScreen gameOverScreen;
        
        public GameManager(IHealth health, GameOverScreen gameOverScreen)
        {
            this.health = health;
            this.gameOverScreen = gameOverScreen;
        }
        
        public void Initialize()
        {
            Time.timeScale = 1;
            
            health.OnDeath += () =>
            {
                Time.timeScale = 0;
                gameOverScreen.gameObject.SetActive(true);
            };
        }
    }
}