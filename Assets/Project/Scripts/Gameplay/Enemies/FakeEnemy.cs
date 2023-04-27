using UnityEngine;
using TMPro;

using RunnerAirplane.Core.Pool;
using RunnerAirplane.Gameplay.Bullets;
using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Enemies
{
    public class FakeEnemy : MonoBehaviour
    {
        [SerializeField] protected int _health;
        [SerializeField] protected int _currentHealth;
        [SerializeField] private TMP_Text _textHealth;
        
        [Space]
        [SerializeField] protected PoolBullets _poolBullets;
        protected BulletType _bulletType;
        
        protected PlayerFakeCombat _playerFakeCombat;

        [SerializeField] private float _combatEndDistance;
        [SerializeField] private float _delayStartFire;
        
        protected bool _isActiveCombat;
        protected float _startFireTime;

        private float _combatDistance;
        protected float _remainingCombatDistance;
        
        protected float _distanceTraveled => 1f - _remainingCombatDistance / _combatDistance;
        
        public bool IsActiveCombat => _isActiveCombat;

        protected virtual void Awake()
        {
            SetHealth(_health);
        }

        protected virtual void Update()
        {
            UpdateDistance();
        }

        private void UpdateDistance()
        {
            if (!_isActiveCombat)
                return;
            
            _remainingCombatDistance = Mathf.Abs(transform.position.z - (_playerFakeCombat.transform.position.z + _combatEndDistance));
        }

        protected void SetHealth(int value)
        {
            _currentHealth = value;
            _textHealth.text = _currentHealth.ToString();
            
            if (_currentHealth <= 0)
                EndCombat();
        }

        public void StartCombat(PlayerFakeCombat playerFakeCombat)
        {
            _playerFakeCombat = playerFakeCombat;
            _startFireTime = Time.time + _delayStartFire;
            _isActiveCombat = true;
            
            _combatDistance = Mathf.Abs(transform.position.z - (_playerFakeCombat.transform.position.z + _combatEndDistance));
        }

        private void EndCombat()
        {
            if (_isActiveCombat)
            {
                _playerFakeCombat.EndCombat(_health);
                Destroy(gameObject);
            }
        }

        public void Die(float delayDie)
        {
            Destroy(gameObject, delayDie);
        }
    }
}
