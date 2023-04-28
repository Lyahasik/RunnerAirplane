using UnityEngine;

using RunnerAirplane.Core.Pool;
using RunnerAirplane.Gameplay.Bullets;

namespace RunnerAirplane.Gameplay.Weapons
{
    public class MachineGun : MonoBehaviour
    {
        [SerializeField] private PoolBullets _poolBullets;
        [SerializeField] private BulletType _bulletType;

        [Space]
        [SerializeField] private GameObject _muzzle;
        [SerializeField] private float _delayFire;
        private float _nextFireTime;

        private bool _isActive = true;

        public bool IsActive
        {
            set
            {
                _nextFireTime = Time.time + _delayFire;
                _isActive = value;
            }
        }

        private void Update()
        {
            Fire();
        }

        private void Fire()
        {
            if (!_isActive
                || _nextFireTime > Time.time)
                return;

            Bullet bullet = _poolBullets.GetBullet(_bulletType);
            bullet.Init(_muzzle.transform.position, _muzzle.transform.forward);

            _nextFireTime = Time.time + _delayFire;
        }
        
        
    }
}
