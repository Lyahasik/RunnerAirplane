using UnityEngine;

using RunnerAirplane.Core;
using RunnerAirplane.Core.Audio;
using RunnerAirplane.Core.Pool;
using RunnerAirplane.Gameplay.Bullets;
using AudioType = RunnerAirplane.Core.Audio.AudioType;

namespace RunnerAirplane.Gameplay.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        private AudioHandler _audioHandler;
        
        [SerializeField] protected PoolBullets _poolBullets;
        [SerializeField] protected BulletType _bulletType;
        [SerializeField] protected AudioType _audioType;
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

        public void Init(AudioHandler audioHandler)
        {
            _audioHandler = audioHandler;
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
            
            if (_audioHandler)
                _audioHandler.PlayBattleSound(_audioType);

            _nextFireTime = Time.time + _delayFire;
        }
    }
}
