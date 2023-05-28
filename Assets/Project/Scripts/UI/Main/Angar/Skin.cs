using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using RunnerAirplane.Gameplay.Progress;
using TMPro;

namespace RunnerAirplane.UI.Main.Angar
{
    public class Skin : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, ISelectHandler
    {
        [SerializeField] private AngarWindow _angarWindow;

        [Space]
        [SerializeField] private int _price;
        [SerializeField] private GameObject _priceObject;
        [SerializeField] private TMP_Text _priceText;
        
        [Space]
        [SerializeField] private int _eraNumber;
        [SerializeField] private int _skinNumber;
        
        [Space]
        [SerializeField] private Image _icon;
        [SerializeField] private Image _frameImage;

        private bool _isUnlock;
        private bool _isActive;

        private Vector2 _pointDown;

        public int Price => _price;

        public int EraNumber => _eraNumber;
        public int SkinNumber => _skinNumber;

        public Sprite SpriteIcon => _icon.sprite;
        public Color Color => _icon.color;

        public bool IsUnlock => _isUnlock;

        private void OnEnable()
        {
            TryUnlock();
            TryActivate();
            
            ProcessingProgress.OnActiveSkin += UpdateActiveSkin;
        }

        private void OnDisable()
        {
            ProcessingProgress.OnActiveSkin -= UpdateActiveSkin;
        }

        private void UpdateActiveSkin(int eraNumber, int skinNumber)
        {
            if (_eraNumber != eraNumber)
                return;
            
            if (_skinNumber == skinNumber)
            {
                _isActive = true;
                _frameImage.enabled = true;
            }
            else
            {
                _isActive = false;
                _frameImage.enabled = false;
            }
        }

        public void TryUnlock()
        {
            _isUnlock = ProcessingProgress.CheckUnlockSkin(_eraNumber, _skinNumber);
            
            if (!_isUnlock)
            {
                _priceObject.SetActive(true);
                _priceText.text = _price.ToString();
            }
        }

        public void TryActivate()
        {
            _isActive = ProcessingProgress.CheckActivateSkin(_eraNumber, _skinNumber);

            if (_isActive)
            {
                _frameImage.enabled = true;
            }
        }

        public void Unlock()
        {
            _isUnlock = true;
            
            _priceObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Down");
            _pointDown = eventData.pressPosition;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("Up");

            if (Input.touchCount == 0
                && (_isUnlock
                || ProcessingProgress.GetNumberMoney() >= _price))
                EventSystem.current.SetSelectedGameObject(gameObject, eventData);
        }

        public void OnSelect(BaseEventData eventData)
        {
            _angarWindow.UpdateSkin(this);
        }
    }
}
