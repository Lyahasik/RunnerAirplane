using UnityEngine;

using RunnerAirplane.Core;
using RunnerAirplane.Gameplay.Bullets;

namespace RunnerAirplane.Gameplay.Weapons
{
    public class RocketLauncher : Weapon
    {
        [SerializeField] private FiringZone _firingZone;
        [SerializeField] private float _minDistanceFire;
        [SerializeField] private float _maxDistanceFire;

        private bool _isExplosive;
        protected override void Fire()
        {
            if (!_isActive
                || _nextFireTime > Time.time)
                return;

            Bullet bullet = _poolBullets.GetBullet(_bulletType);
            float distance = Random.Range(_minDistanceFire, _maxDistanceFire);
            
            if (_isExplosive)
                bullet.Init(_firingZone, _muzzle.transform.position, _muzzle.transform.forward, distance, _damage);
            else
                bullet.Init(_muzzle.transform.position, _muzzle.transform.forward, _damage);

            _nextFireTime = Time.time + _delayFire;
        }
    }
}
