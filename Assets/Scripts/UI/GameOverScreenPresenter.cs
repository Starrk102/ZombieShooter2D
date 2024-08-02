using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace UI
{
    public class GameOverScreenPresenter : IInitializable
    {
        private GameOverScreen gameOverScreen;
        
        public GameOverScreenPresenter(GameOverScreen gameOverScreen)
        {
            this.gameOverScreen = gameOverScreen;
          
        }
        
        public void Initialize()
        {
            gameOverScreen.restart.onClick.AddListener(() =>
            {
                Debug.Log("Scene loading");
                SceneManager.LoadScene(0);
            });
            
            gameOverScreen.exit.onClick.AddListener(Application.Quit);
        }
    }
}