using UnityEngine;

using RunnerAirplane.Gameplay.Progress;

namespace RunnerAirplane.UI.Main
{
    public class MainWindow : MonoBehaviour
    {
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
        
        public void ResetGame()
        {
            ProcessingProgress.ResetAll();
            ProcessingProgress.UpdateNumberMoney(1000);
        }
    }
}
