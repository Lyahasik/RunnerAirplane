using System;
using UnityEngine;

using RunnerAirplane.Gameplay.Bullets;

namespace RunnerAirplane.Gameplay.Enemies
{
    public class Helicopter : FakeEnemy
    {
        [Space]
        [SerializeField] private GameObject _weapon;
        [SerializeField] private float _shotDelay;
        [SerializeField] private float _delayDamage;

        private float _nextFireTime;

        protected override void Awake()
        {
            base.Awake();
            
            _bulletType = BulletType.FakeBullet;
        }

        protected override void Update()
        {
            if (!_playerFakeCombat)
                return;
            
            base.Update();

            UpdateHealth();
            Fire();
        }

        private void UpdateHealth()
        {
            if (!_isActiveCombat)
                return;
            
            int newHealth = _health - (int) Math.Ceiling(_health * _distanceTraveled);
            SetHealth(newHealth);
        }

        private void Fire()
        {
            if (!_isActiveCombat
                || _startFireTime > Time.time
                || _nextFireTime > Time.time)
                return;

            Bullet bullet = _poolBullets.GetBullet(_bulletType);
            bullet.Init(_weapon.transform.position, _playerFakeCombat.transform);
            Invoke(nameof(MakeDamage), _delayDamage);

            _nextFireTime = Time.time + _shotDelay;
        }

        private void MakeDamage()
        {
            if (!_playerFakeCombat)
                return;
            
            _playerFakeCombat.TemporaryHealth = _health - _currentHealth;
        }
    }
}
