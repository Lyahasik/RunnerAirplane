using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

using RunnerAirplane.Core;
using RunnerAirplane.UI.Main;

namespace RunnerAirplane.UI.Level
{
    public class LevelMenu : MonoBehaviour
    {
        private SceneController _sceneController;

        [Space]
        [SerializeField] private TMP_Text _textLevel;
        [SerializeField] private GameObject _menuWindow;
        
        [Space]
        [SerializeField] private GameObject _successGameWindow;
        [SerializeField] private TMP_Text _textSuccessLevel;
        
        [Space]
        [SerializeField] private GameObject _gameOverWindow;
        [SerializeField] private Wallet _wallet;

        private void Start()
        {
            _sceneController = FindObjectOfType<SceneController>();
            
            int levelNumber = SceneManager.GetActiveScene().buildIndex;
            
            _textLevel.text = $"Level {levelNumber}";
            _textSuccessLevel.text = $"LEVEL {levelNumber}";
        }

        public void SuccessGame(int money)
        {
            _successGameWindow.SetActive(true);
            _wallet.SetMoney(money);
        }

        public void GameOver()
        {
            _gameOverWindow.SetActive(true);
        }
        
        public void Restart()
        {
            _sceneController.Restart();
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
