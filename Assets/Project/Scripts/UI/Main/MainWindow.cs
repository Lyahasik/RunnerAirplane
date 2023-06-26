using UnityEngine;

using RunnerAirplane.Gameplay.Progress;

namespace RunnerAirplane.UI.Main
{
    public class MainWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _presentation;

        [Space]
        [SerializeField] private GameObject _buttonNoAds;
        [SerializeField] private Settings _settings;

        private void OnEnable()
        {
            if (_presentation)
                _presentation.SetActive(true);
            
            if (AdsHandler.IsPurchased)
                _buttonNoAds.SetActive(false);
            
            _settings.UpdateSettings();
        }

        private void OnDisable()
        {
            if (_presentation)
                _presentation.SetActive(false);
        }

        public void AdsToBuy()
        {
            IapHandler.Single.BuyNoAds();
            _buttonNoAds.SetActive(false);
        }

        public void ResetGame()
        {
            ProcessingProgress.ResetAll();
        }
    }
}
