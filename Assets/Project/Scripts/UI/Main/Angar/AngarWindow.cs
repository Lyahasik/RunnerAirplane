using System;
using UnityEngine;
using UnityEngine.UI;

using RunnerAirplane.Gameplay.Progress;
using RunnerAirplane.ScriptableObjects;

namespace RunnerAirplane.UI.Main.Angar
{
    public class AngarWindow : MonoBehaviour
    {
        [SerializeField] private Image _selectedSkinImage;
        [SerializeField] private FullListEraData _fullListEraData;
        [SerializeField] private ListSelectedEraData _listSelectedEraData;

        [Space]
        [SerializeField] private GameObject _buttonBuy;
        [SerializeField] private GameObject _buttonActive;

        private Skin _currentSkin;
        
        public static event Action<int, int> OnSelectSkin;

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
            
            OnSelectSkin?.Invoke(_currentSkin.EraNumber, _currentSkin.SkinNumber);
        }

        private void UpdateSkinPrefab(int eraNumber, int skinNumber)
        {
            int eraIndex = eraNumber - 1;
            int skinIndex = skinNumber - 1;
            
            foreach (ListEraData listEraData in _fullListEraData.ListsEra)
            {
                if (listEraData.Number == eraNumber)
                {
                    _listSelectedEraData.ListEra[eraIndex].Prefab = listEraData.ListPrefabs[skinIndex];
                        
                    break;
                }
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
                _buttonActive.SetActive(false);
            }
            else if (_currentSkin.IsUnlock)
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

        public void UnlockSkin()
        {
            int numberMoney = ProcessingProgress.GetNumberMoney();
            
            if (numberMoney < _currentSkin.Price)
                return;

            ProcessingProgress.UpdateNumberMoney(-_currentSkin.Price);
            ProcessingProgress.UnlockSkin(_currentSkin.EraNumber, _currentSkin.SkinNumber);
            _currentSkin.Unlock();
            SetSkinImage(_currentSkin.SpriteIcon, _currentSkin.Color);
            
            _buttonBuy.SetActive(false);
            _buttonActive.SetActive(true);
            
            OnSelectSkin?.Invoke(_currentSkin.EraNumber, _currentSkin.SkinNumber);
        }

        public void ActivateSkin()
        {
            ProcessingProgress.ActivateSkin(_currentSkin.EraNumber, _currentSkin.SkinNumber);
            UpdateSkinPrefab(_currentSkin.EraNumber, _currentSkin.SkinNumber);
            
            _buttonActive.SetActive(false);
        }
    }
}
