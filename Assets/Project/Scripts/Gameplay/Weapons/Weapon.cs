using UnityEngine;

using RunnerAirplane.Core;
using RunnerAirplane.Core.Pool;
using RunnerAirplane.Gameplay.Bullets;

namespace RunnerAirplane.Gameplay.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected PoolBullets _poolBullets;
        [SerializeField] protected BulletType _bulletType;
        [SerializeField] protected bool _isPlayerWeapon;

        [Space]
        [SerializeField] protected GameObject _muzzle;
        [SerializeField] protected int _damage;
        [SerializeField] protected float _delayFire;
        protected float _nextFireTime;

        protected bool _isActive;

        public virtual bool IsActive
        {
            set
            {
                _nextFireTime = Time.time + _delayFire;
                _isActive = value;
            }
        }

        protected virtual void Update()
        {
            if (LevelHandler.PauseGame)
                return;
            
            Fire();
        }

        protected virtual void Fire()
        {
            if (!_isActive
                || _nextFireTime > Time.time
                || !_poolBullets)
                return;

            Bullet bullet = _poolBullets.GetBullet(_bulletType);
            bullet.Init(_muzzle.transform.position, _muzzle.transform.forward, _damage, _isPlayerWeapon);

            _nextFireTime = Time.time + _delayFire;
        }
    }
}
