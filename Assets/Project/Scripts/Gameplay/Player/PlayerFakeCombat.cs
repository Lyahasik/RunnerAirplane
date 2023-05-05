using UnityEngine;

using RunnerAirplane.Core.Pool;
using RunnerAirplane.Gameplay.Bullets;
using RunnerAirplane.Gameplay.Enemies;
using RunnerAirplane.Gameplay.Objects;

namespace RunnerAirplane.Gameplay.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerData))]
    public class PlayerFakeCombat : MonoBehaviour
    {
        [SerializeField] private PoolBullets _poolBullets;
        
        [Space]
        [SerializeField] private GameObject _gun1;
        [SerializeField] private GameObject _gun2;
        [SerializeField] private float _shotDelay;
        
        [Space]
        [SerializeField] private GameObject _rocketLauncher;
        [SerializeField] private float _delayDieEnemy;
        
        [Space]
        [SerializeField] private GameObject _bombLauncher;
        [SerializeField] private float _delayLaunchBomb;

        private PlayerMovement _playerMovement;
        private PlayerData _playerData;
        private FakeEnemy _fakeEnemy;
        private FakeEnemy _diedEnemy;
        private bool _enemyHelicopter;
        private bool _enemyAirDefence;

        private bool _isActiveCombat;
        private float _timeEndCombat;
        private float _nextFireTime;

        private float _startLaunchBomb;
        
        private bool _isEmpty;

        public PoolBullets PoolBullets => _poolBullets;

        public bool IsActiveCombat => _isActiveCombat;
        
        public int TemporaryHealth
        {
            set => _playerData.TemporaryHealth = value;
        }

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerData = GetComponent<PlayerData>();
        }

        private void Update()
        {
            FireGun();
            FireBombLauncher();
        }

        public bool TryStartCombat(FakeEnemy fakeEnemy)
        {
            if (_diedEnemy == fakeEnemy)
                return false;
            
            _fakeEnemy = fakeEnemy;
            
            if (_playerData.IsPresenceDisposableRocket)
            {
                FireDisposableRocket();
                return false;
            }
            
            if (_fakeEnemy is Helicopter)
                _enemyHelicopter = true;
            if (_fakeEnemy is AirDefense)
            {
                _enemyAirDefence = true;
                _startLaunchBomb = Time.time + _delayLaunchBomb;
            }
            
            _isActiveCombat = true;
            _playerMovement.IsFakeCombating = true;

            return true;
        }

        private void FireDisposableRocket()
        {
            if (!_fakeEnemy)
                return;

            Bullet bullet = _poolBullets.GetBullet(BulletType.FakeRocket);
            bullet.Init(_bombLauncher.transform.position, _fakeEnemy.transform);
            _fakeEnemy.Die(_delayDieEnemy);
            _diedEnemy = _fakeEnemy;
            _fakeEnemy = null;
            
            _playerData.IsPresenceDisposableRocket = false;
        }

        public void EndCombat(int damage)
        {
            _fakeEnemy = null;
            _enemyHelicopter = false;
            _enemyAirDefence = false;
            
            _isActiveCombat = false;
            _isEmpty = false;
            _playerMovement.IsFakeCombating = false;
            _playerData.TemporaryHealth = 0;
            _playerData.CalculateNewHealth(MathOperationType.Subtraction, damage);
        }

        private void FireGun()
        {
            if (!_isActiveCombat
                || !_fakeEnemy
                || !_enemyHelicopter
                || _nextFireTime > Time.time)
                return;

            Bullet bullet1 = _poolBullets.GetBullet(BulletType.FakeBullet);
            Bullet bullet2 = _poolBullets.GetBullet(BulletType.FakeBullet);
            bullet1.Init(_gun1.transform.position, _fakeEnemy.transform);
            bullet2.Init(_gun2.transform.position, _fakeEnemy.transform);

            _nextFireTime = Time.time + _shotDelay;
        }

        private void FireBombLauncher()
        {
            if (!_isActiveCombat
                || !_fakeEnemy
                || !_enemyAirDefence
                || _startLaunchBomb > Time.time
                || _isEmpty)
                return;

            Bullet bullet = _poolBullets.GetBullet(BulletType.FakeFlyingBomb);
            bullet.Init(_bombLauncher.transform.position, _fakeEnemy.transform);
            
            _isEmpty = true;
        }
    }
}
