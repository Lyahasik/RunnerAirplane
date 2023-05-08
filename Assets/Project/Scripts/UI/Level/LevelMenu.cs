using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

using RunnerAirplane.Core;
using RunnerAirplane.UI.Main;

namespace RunnerAirplane.UI.Level
{
    public class LevelMenu : MonoBehaviour
    {
        [SerializeField] private SceneController _sceneController;

        [Space]
        [SerializeField] private TMP_Text _textLevel;
        [SerializeField] private GameObject _endGameWindow;
        [SerializeField] private Wallet _wallet;

        private void Start()
        {
            _textLevel.text = $"Level {SceneManager.GetActiveScene().buildIndex}";
        }

        public void EndGame(int money)
        {
            _endGameWindow.SetActive(true);
            _wallet.SetMoney(money);
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
