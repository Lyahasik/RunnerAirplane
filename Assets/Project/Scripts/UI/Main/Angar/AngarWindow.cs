using UnityEngine;
using UnityEngine.UI;

using RunnerAirplane.Core.Audio;
using RunnerAirplane.Gameplay.Progress;
using RunnerAirplane.Gameplay.Player;
using RunnerAirplane.ScriptableObjects;

using AudioType = RunnerAirplane.Core.Audio.AudioType;

namespace RunnerAirplane.UI.Main.Angar
{
    public class AngarWindow : MonoBehaviour
    {
        private AudioHandler _audioHandler;
        private PlayerData _playerData;
        
        [SerializeField] private Image _selectedSkinImage;
        [SerializeField] private FullListEraData _fullListEraData;
        [SerializeField] private ListSelectedEraData _listSelectedEraData;

        private Skin _currentSkin;

        private void Awake()
        {
            ProcessingProgress.PrepareSkins();
        }

        private void Start()
        {
            _audioHandler = FindObjectOfType<AudioHandler>();
            _playerData = FindObjectOfType<PlayerData>();
        }

        private void OnDisable()
        {
            _currentSkin = null;
        }

        public void UpdateSkin(Skin skin = null)
        {
            _currentSkin = skin;

            if (_currentSkin)
            {
                SetSkinImage(_currentSkin.SpriteIcon, _currentSkin.Color);
                UnlockSkin();
                ActivateSkin();
            }
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

        public void UnlockSkin()
        {
            int numberMoney = ProcessingProgress.GetNumberMoney();
            
            if (numberMoney < _currentSkin.Price)
                return;

            ProcessingProgress.UpdateNumberMoney(-_currentSkin.Price);
            ProcessingProgress.UnlockSkin(_currentSkin.EraNumber, _currentSkin.SkinNumber);
            _currentSkin.Unlock();
            SetSkinImage(_currentSkin.SpriteIcon, _currentSkin.Color);
        }

        public void ActivateSkin()
        {
            if (!_currentSkin.IsUnlock)
                return;
            
            _audioHandler.PlayBaseSound(AudioType.SoundSelectSkin);
            
            ProcessingProgress.ActivateSkin(_currentSkin.EraNumber, _currentSkin.SkinNumber);
            UpdateSkinPrefab(_currentSkin.EraNumber, _currentSkin.SkinNumber);
            
            _playerData.TryUpdateSkin();
        }
    }
}
