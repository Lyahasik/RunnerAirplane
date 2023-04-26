using UnityEngine;

using RunnerAirplane.Gameplay.Bullets;

namespace RunnerAirplane.Gameplay.Enemies
{
    public class Helicopter : Enemy
    {
        [Space]
        [SerializeField] private GameObject _weapon;
        [SerializeField] private float _shotDelay;

        private float _nextFireTime;

        private void Awake()
        {
            _bulletType = BulletType.FakeBullet;
        }

        protected override void Update()
        {
            Fire();
            
            base.Update();
        }

        private void Fire()
        {
            if (!_isActiveCombat
                || _startFireTime > Time.time
                || _nextFireTime > Time.time)
                return;

            Bullet bullet = _poolBullets.GetBullet(_bulletType);
            bullet.Init(_weapon.transform.position, _playerFakeCombat.transform);

            _nextFireTime = Time.time + _shotDelay;
        }
    }
}
