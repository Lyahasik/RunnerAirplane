using System.Linq;
using UnityEngine;
using TMPro;

using RunnerAirplane.Gameplay.Objects;
using RunnerAirplane.ScriptableObjects;

namespace RunnerAirplane.Gameplay.Player
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private int _startHealth;
        [SerializeField] private TMP_Text _textHealth;

        [SerializeField] private ListSelectedEraData _listSelectedEra;
        private GameObject _currentPrefabEra;

        private int _currentHealth;
        private int _temporaryHealth;
        private int _currentEra;

        private bool _isPresenceDisposableRocket;

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
            GameObject prefabEra = _listSelectedEra.ListEra.First(data =>
            {
                return _currentHealth >= data.MinValue && _currentHealth <= data.MaxValue;
            }).Prefab;

            if (prefabEra == _currentPrefabEra)
                return;

            if (_currentPrefabEra)
                Destroy(_currentPrefabEra);
            
            _currentPrefabEra = Instantiate(prefabEra, transform);
            _currentPrefabEra.transform.parent = transform;
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
