using UnityEngine;

using RunnerAirplane.Core;
using RunnerAirplane.Gameplay.Progress;

namespace RunnerAirplane.UI.Main
{
    public class MainWindow : MonoBehaviour
    {
        private const int _firstLevelIndex = 1;
        
        [SerializeField] private SceneController _sceneController;
        [SerializeField] private GameObject _presentation;

        [SerializeField] private Settings _settings;

        private void OnEnable()
        {
            if (_presentation)
                _presentation.SetActive(true);
            _settings.UpdateSettings();
        }

        private void OnDisable()
        {
            if (_presentation)
                _presentation.SetActive(false);
        }

        public void StartGame()
        {
            int index = ProcessingProgress.GetLastLevel() + 1;

            if (index < _firstLevelIndex)
                index = _firstLevelIndex;
                
            _sceneController.StartIndexScene(index);
        }
        
        public void ResetGame()
        {
            ProcessingProgress.ResetAll();
            ProcessingProgress.UpdateNumberMoney(1000);
        }
    }
}
