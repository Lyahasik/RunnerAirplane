using UnityEngine;
using TMPro;

using RunnerAirplane.Core.Audio;
using RunnerAirplane.Core.Pool;
using RunnerAirplane.Gameplay.Bullets;
using RunnerAirplane.Gameplay.Player;
using AudioType = RunnerAirplane.Core.Audio.AudioType;

namespace RunnerAirplane.Gameplay.Enemies
{
    public class FakeEnemy : MonoBehaviour
    {
        private AudioHandler _audioHandler;
        
        [SerializeField] protected int _health;
        [SerializeField] protected int _currentHealth;
        [SerializeField] private TMP_Text _textHealth;
        
        [Space]
        [SerializeField] protected PoolBullets _poolBullets;
        protected BulletType _bulletType;
        
        protected PlayerFakeCombat _playerFakeCombat;

        [SerializeField] private float _combatEndDistance;
        [SerializeField] private float _delayStartFire;
        [SerializeField] private Vector3 _explosionShiftPosition;
        
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

        protected virtual void Start()
        {
            _audioHandler = FindObjectOfType<AudioHandler>();
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
                
                Die(0f);
            }
        }

        public void Die(float delayDie)
        {
            _audioHandler.PlayBaseSound(AudioType.SoundExplosionTechnique);
            
            Invoke(nameof(Explosion), delayDie);
            Destroy(gameObject, delayDie);
        }

        private void Explosion()
        {
            Bullet bullet = _poolBullets.GetBullet(BulletType.ExplosionEffect);
            bullet.Init(transform.position + _explosionShiftPosition);
            bullet.transform.parent = transform.parent;
        }
    }
}
