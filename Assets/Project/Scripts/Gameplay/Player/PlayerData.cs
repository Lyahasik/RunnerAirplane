using UnityEngine;
using TMPro;

using RunnerAirplane.Gameplay.Objects;

namespace RunnerAirplane.Gameplay.Player
{
    public class PlayerData : MonoBehaviour
    {
        [SerializeField] private int _startHealth;
        [SerializeField] private TMP_Text _textHealth;

        private int _currentHealth;
        private int _temporaryHealth;
        private int _currentEra;

        public int CurrentHealth => _currentHealth;

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
            // Debug.Log($"Era {_currentEra}");
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
