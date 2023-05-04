using UnityEngine;

using RunnerAirplane.Core;

namespace RunnerAirplane.UI
{
    public class LevelMenu : MonoBehaviour
    {
        [SerializeField] private SceneController _sceneController;
        
        [Space]
        [SerializeField] private GameObject _endGameWindow;

        public void EndGame()
        {
            _endGameWindow.SetActive(true);
        }
        
        public void Restart()
        {
            _sceneController.Restart();
        }
        
        public void StartMainScene()
        {
            _sceneController.StartMainScene();
        }

        public void PreviousScene()
        {
            SceneController.PreviousScene();
        }

        public void NextScene()
        {
            SceneController.NextScene();
        }
    }
}
