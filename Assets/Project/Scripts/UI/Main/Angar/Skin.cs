using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using RunnerAirplane.Gameplay.Progress;

namespace RunnerAirplane.UI.Main.Angar
{
    public class Skin : MonoBehaviour, IPointerDownHandler, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private AngarWindow _angarWindow;
        
        [Space]
        [SerializeField] private int _eraNumber;
        [SerializeField] private int _skinNumber;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _frameImage;

        private bool _isActive;

        public int EraNumber => _eraNumber;
        public int SkinNumber => _skinNumber;

        public Sprite SpriteIcon => _icon.sprite;
        public Color Color => _icon.color;

        public bool IsActive => _isActive;

        private void OnEnable()
        {
            TryActive();
        }

        public void TryActive()
        {
            _isActive = ProcessingProgress.CheckSkin(_eraNumber, _skinNumber);
            
            _icon.color = _isActive ? Color.white : Color.black;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(gameObject, eventData);
        }

        public void OnSelect(BaseEventData eventData)
        {
            _frameImage.enabled = true;
            _angarWindow.UpdateSkin(this);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            _frameImage.enabled = false;
        }
    }
}
