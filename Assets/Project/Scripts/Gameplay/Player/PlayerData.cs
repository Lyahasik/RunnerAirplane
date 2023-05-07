using System.Linq;
using UnityEngine;
using TMPro;

using RunnerAirplane.Core.Pool;
using RunnerAirplane.Gameplay.Bullets;
using RunnerAirplane.Gameplay.Objects;
using RunnerAirplane.ScriptableObjects;

namespace RunnerAirplane.Gameplay.Player
{
    public class PlayerData : MonoBehaviour
    {
        private PoolBullets _poolBullets;
        
        [SerializeField] private int _startHealth;
        [SerializeField] private TMP_Text _textHealth;
        [SerializeField] private float _explosionScale;

        [SerializeField] private ListSelectedEraData _listSelectedEra;
        private GameObject _currentPrefabEra;

        private int _currentHealth;
        private int _temporaryHealth;
        private int _currentEra;

        private bool _isPresenceDisposableRocket;

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
                    UpdateHealth(_currentHealth - value);
                    break;
                case MathOperationType.Multiplication:
                    UpdateHealth(_currentHealth * value);
                    break;
                case MathOperationType.Division:
                    UpdateHealth(_currentHealth / value);
                    break;
            }
        }

        private void UpdateHealth(int value)
        {
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
            GameObject prefabEra = _listSelectedEra.ListEra
                .First(data => _currentHealth >= data.MinValue && _currentHealth <= data.MaxValue)
                .Prefab;

            if (prefabEra == _currentPrefabEra)
                return;

            if (_currentPrefabEra)
                Destroy(_currentPrefabEra);
            
            _currentPrefabEra = Instantiate(prefabEra, transform);
            _currentPrefabEra.transform.parent = transform;
        }
        
        private void Explosion()
        {
            Bullet bullet = _poolBullets.GetBullet(BulletType.Explosion);
            bullet.Init(transform.position);
            bullet.transform.localScale = new Vector3(_explosionScale, _explosionScale, _explosionScale);
        }

        private void Die()
        {
            Explosion();
            Destroy(gameObject);
        }
    }
}
