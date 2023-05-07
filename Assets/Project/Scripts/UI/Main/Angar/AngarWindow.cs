using UnityEngine;
using UnityEngine.UI;

using RunnerAirplane.Gameplay.Progress;

namespace RunnerAirplane.UI.Main.Angar
{
    public class AngarWindow : MonoBehaviour
    {
        [SerializeField] private Image _selectedSkinImage;

        [Space]
        [SerializeField] private GameObject _buttonBuy;
        [SerializeField] private GameObject _buttonActive;

        private Skin _currentSkin;

        private void OnDisable()
        {
            _currentSkin = null;
        }

        public void UpdateSkin(Skin skin = null)
        {
            _currentSkin = skin;

            UpdateButton();
            if (_currentSkin)
            {
                SetSkinImage(_currentSkin.SpriteIcon, _currentSkin.Color);
            }
        }

        private void SetSkinImage(Sprite sprite, Color color)
        {
            _selectedSkinImage.sprite = sprite;
            _selectedSkinImage.color = color;
        }

        private void UpdateButton()
        {
            if (!_currentSkin)
            {
                _buttonBuy.SetActive(false);
                _buttonActive.SetActive(false);
                return;
            }

            if (_currentSkin.IsActive)
            {
                _buttonBuy.SetActive(false);
                _buttonActive.SetActive(true);
            }
            else
            {
                _buttonBuy.SetActive(true);
                _buttonActive.SetActive(false);
            }
        }

        public void ActivateSkin()
        {
            ProcessingProgress.ActivateSkin(_currentSkin.EraNumber, _currentSkin.SkinNumber);
            _currentSkin.TryActive();
            SetSkinImage(_currentSkin.SpriteIcon, _currentSkin.Color);
            
            _buttonBuy.SetActive(false);
            _buttonActive.SetActive(true);
        }
    }
}
