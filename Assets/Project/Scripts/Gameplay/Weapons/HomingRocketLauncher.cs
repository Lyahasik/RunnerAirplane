using UnityEngine;

using RunnerAirplane.Gameplay.Bullets;
using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Weapons
{
    public class HomingRocketLauncher : Weapon
    {
        [SerializeField] private PlayerMovement _playerMovement;
        protected override void Fire()
        {
            if (!_isActive
                || !_playerMovement
                || _nextFireTime > Time.time)
                return;

            Bullet bullet = _poolBullets.GetBullet(_bulletType);
            bullet.Init(_muzzle.transform.position, _muzzle.transform.forward, _playerMovement.transform, _damage);

            _nextFireTime = Time.time + _delayFire;
        }
    }
}
