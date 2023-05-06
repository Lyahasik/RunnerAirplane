using System;
using UnityEngine;
using TMPro;

using RunnerAirplane.Core;
using UnityEngine.SceneManagement;

namespace RunnerAirplane.UI
{
    public class LevelMenu : MonoBehaviour
    {
        [SerializeField] private SceneController _sceneController;

        [Space]
        [SerializeField] private TMP_Text _textLevel;
        [SerializeField] private GameObject _endGameWindow;

        private void Start()
        {
            _textLevel.text = $"Level {SceneManager.GetActiveScene().buildIndex}";
        }

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
