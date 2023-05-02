using TMPro;
using UnityEngine;

namespace RunnerAirplane.Gameplay.Bosses
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class BossData : MonoBehaviour
    {
        [SerializeField] private int _startHealth;
        [SerializeField] private TMP_Text _textHealth;

        private int _currentHealth;

        private void Awake()
        {
            _currentHealth = _startHealth;
        }

        private void Start()
        {
            UpdateHealth(_startHealth);
        }

        public void CalculateNewHealth(int value)
        {
            UpdateHealth(_currentHealth - value);
        }

        private void UpdateHealth(int value)
        {
            _currentHealth = value;

            _textHealth.text = _currentHealth.ToString();

            if (_currentHealth <= 0)
                Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
