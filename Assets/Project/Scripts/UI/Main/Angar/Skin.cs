using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using RunnerAirplane.Gameplay.Progress;

namespace RunnerAirplane.UI.Main.Angar
{
    public class Skin : MonoBehaviour, IPointerDownHandler, ISelectHandler
    {
        [SerializeField] private AngarWindow _angarWindow;
        
        [Space]
        [SerializeField] private int _eraNumber;
        [SerializeField] private int _skinNumber;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _frameImage;

        private bool _isUnlock;
        private bool _isActive;

        public int EraNumber => _eraNumber;
        public int SkinNumber => _skinNumber;

        public Sprite SpriteIcon => _icon.sprite;
        public Color Color => _icon.color;

        public bool IsUnlock => _isUnlock;
        public bool IsActive => _isActive;

        private void OnEnable()
        {
            TryUnlock();
            TryActivate();
            
            ProcessingProgress.OnActiveSkin += UpdateActiveSkin;
            AngarWindow.OnSelectSkin += UpdateSelectSkin;
        }

        private void OnDisable()
        {
            ProcessingProgress.OnActiveSkin -= UpdateActiveSkin;
            AngarWindow.OnSelectSkin -= UpdateSelectSkin;
        }

        private void UpdateActiveSkin(int eraNumber, int skinNumber)
        {
            if (_eraNumber != eraNumber)
                return;

            if (_skinNumber == skinNumber)
            {
                _isActive = true;
                _frameImage.color = Color.blue;
                _frameImage.enabled = true;
            }
            else
            {
                _isActive = false;
                
                _frameImage.enabled = false;
            }
        }

        private void UpdateSelectSkin(int eraNumber, int skinNumber)
        {
            if (_isActive)
                return;
            
            if (_eraNumber == eraNumber
                && _skinNumber == skinNumber)
            {
                _frameImage.color = Color.green;
                _frameImage.enabled = true;
            }
            else
            {
                _frameImage.enabled = false;
            }
        }

        public void TryUnlock()
        {
            _isUnlock = ProcessingProgress.CheckUnlockSkin(_eraNumber, _skinNumber);
            
            _icon.color = _isUnlock ? Color.white : Color.black;
        }

        public void TryActivate()
        {
            _isActive = ProcessingProgress.CheckActivateSkin(_eraNumber, _skinNumber);

            if (_isActive)
            {
                _frameImage.color = Color.blue;
                _frameImage.enabled = true;
            }
            else
            {
                _frameImage.enabled = false;
            }
        }

        public void Unlock()
        {
            _isUnlock = true;
            
            _icon.color = Color.white;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(gameObject, eventData);
        }

        public void OnSelect(BaseEventData eventData)
        {
            _angarWindow.UpdateSkin(this);
        }
    }
}
