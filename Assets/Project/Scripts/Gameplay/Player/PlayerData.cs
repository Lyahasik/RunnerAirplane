using System.Linq;
using UnityEngine;
using TMPro;

using EasyHaptic_EvilBurgers;

using RunnerAirplane.Core;
using RunnerAirplane.Core.Audio;
using RunnerAirplane.Core.Pool;
using RunnerAirplane.Gameplay.Bullets;
using RunnerAirplane.Gameplay.Objects;
using RunnerAirplane.ScriptableObjects;
using RunnerAirplane.UI.Main;
using AudioType = RunnerAirplane.Core.Audio.AudioType;

namespace RunnerAirplane.Gameplay.Player
{
    public class PlayerData : MonoBehaviour
    {
        private AudioHandler _audioHandler;
        
        private PoolBullets _poolBullets;
        
        [SerializeField] private int _startHealth;
        [SerializeField] private TMP_Text _textHealth;

        [SerializeField] private ListSelectedEraData _listSelectedEra;
        private Technique _currentPrefabEra;

        private int _currentHealth;
        private int _temporaryHealth;
        private int _currentEra;

        private bool _isPresenceDisposableRocket;

        private bool _isInit;

        public int StartHealth => _startHealth;
        public int CurrentHealth => _currentHealth;

        public bool IsPresenceDisposableRocket
        {
            get => _isPresenceDisposableRocket;
            set => _isPresenceDisposableRocket = value;
        }

        public int TemporaryHealth
        {
            set
            {
                _temporaryHealth = value;
                UpdateHealth(_currentHealth);
            }
        }

        private void Awake()
        {
            _currentHealth = _startHealth;
        }

        private void Start()
        {
            _audioHandler = FindObjectOfType<AudioHandler>();
            
            _poolBullets = GetComponent<PlayerFakeCombat>().PoolBullets;
            UpdateHealth(_startHealth);
        }

        public void CalculateNewHealth(MathOperationType operationType, int value)
        {
            switch (operationType)
            {
                case MathOperationType.Addition:
                    UpdateHealth(_currentHealth + value);
                    break;
                case MathOperationType.Subtraction:
                    Vibrate();
                    UpdateHealth(_currentHealth - value);
                    break;
                case MathOperationType.Multiplication:
                    UpdateHealth(_currentHealth * value);
                    break;
                case MathOperationType.Division:
                    Vibrate();
                    UpdateHealth(_currentHealth / value);
                    break;
            }
        }

        private void Vibrate()
        {
            if (!Settings.VibrationOn)
                return;

            CustomVibrationData custom = new CustomVibrationData();

            custom.durationInSeconds = 0.2f;
            custom.amplitude = 10;
            custom.sharpness = 10;
            
            EasyHaptic.PlayCustom(custom);
        }

        private void UpdateHealth(int value)
        {
            if (_isInit && LevelHandler.PauseGame)
                return;
            
            _currentHealth = value;

            int health = _currentHealth - _temporaryHealth;
            _textHealth.text = health.ToString();

            if (health <= 0)
            {
                Die();
                return;
            }
            
            UpdateEra();
        }

        private void UpdateEra()
        {
            if (TryUpdateSkin())
                ActiveAudio();
            
            _isInit = true;
        }

        public bool TryUpdateSkin()
        {
            Technique prefabEra = _listSelectedEra.ListEra
                .First(data => _currentHealth >= data.MinValue && _currentHealth <= data.MaxValue)
                .Prefab;
            
            if (_currentPrefabEra
                && prefabEra.Id == _currentPrefabEra.Id)
                return false;

            if (_currentPrefabEra)
                Destroy(_currentPrefabEra.gameObject);

            _currentPrefabEra = Instantiate(prefabEra, transform);
            _currentPrefabEra.transform.parent = transform;
            
            return true;
        }

        public void ActiveAudio(bool isStart = false)
        {
            if (!_isInit)
                return;
            
            _audioHandler.StopSoundAll();
            
            if (!isStart)
                _audioHandler.PlayBaseSound(AudioType.SoundUpdateEra);
            _audioHandler.PlayBaseSound(_currentPrefabEra.AudioType);
        }
        
        private void Explosion()
        {
            Bullet bullet = _poolBullets.GetBullet(BulletType.ExplosionEffect);
            bullet.Init(transform.position);
        }

        private void Die()
        {
            Explosion();
            Destroy(gameObject);
            
            _audioHandler.StopSoundAll();
            _audioHandler.PlayBaseSound(AudioType.SoundExplosionTechnique);
        }
    }
}
