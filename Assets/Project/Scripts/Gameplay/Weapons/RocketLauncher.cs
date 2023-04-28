using UnityEngine;

using RunnerAirplane.Gameplay.Bullets;

namespace RunnerAirplane.Gameplay.Weapons
{
    public class RocketLauncher : Weapon
    {
        [SerializeField] private float _minDistanceFire;
        [SerializeField] private float _maxDistanceFire;
        protected override void Fire()
        {
            if (!_isActive
                || _nextFireTime > Time.time)
                return;

            Bullet bullet = _poolBullets.GetBullet(_bulletType);
            float distance = Random.Range(_minDistanceFire, _maxDistanceFire);
            bullet.Init(_muzzle.transform.position, _muzzle.transform.forward, distance, _damage);

            _nextFireTime = Time.time + _delayFire;
        }
    }
}
