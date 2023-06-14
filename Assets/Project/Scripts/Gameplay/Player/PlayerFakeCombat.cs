using UnityEngine;

using RunnerAirplane.Core.Pool;
using RunnerAirplane.Gameplay.Bullets;
using RunnerAirplane.Gameplay.Enemies;
using RunnerAirplane.Gameplay.Objects;
using RunnerAirplane.Core.Audio;

using AudioType = RunnerAirplane.Core.Audio.AudioType;

namespace RunnerAirplane.Gameplay.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerData))]
    public class PlayerFakeCombat : MonoBehaviour
    {
        private AudioHandler _audioHandler;
        
        [SerializeField] private PoolBullets _poolBullets;
        
        [Space]
        [SerializeField] private GameObject _gun1;
        [SerializeField] private GameObject _gun2;
        [SerializeField] private float _shotDelay;
        
        [Space]
        [SerializeField] private GameObject _rocketLauncher;
        [SerializeField] private float _delayDieEnemy;
        
        [Space]
        [SerializeField] private float _delayLaunchRocket;

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
        
        private void Start()
        {
            _audioHandler = FindObjectOfType<AudioHandler>();
        }

        private void Update()
        {
            FireGun();
            FireRocketLauncher();
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
                _startLaunchBomb = Time.time + _delayLaunchRocket;
            }
            
            _isActiveCombat = true;
            _playerMovement.IsFakeCombating = true;

            return true;
        }

        private void FireDisposableRocket()
        {
            if (!_fakeEnemy)
                return;

            Bullet bullet = _poolBullets.GetBullet(BulletType.DisposableRocket);
            bullet.Init(_rocketLauncher.transform.position, _fakeEnemy.transform, 0, true);
            _fakeEnemy.Die(_delayDieEnemy);
            _diedEnemy = _fakeEnemy;
            _fakeEnemy = null;
            
            _audioHandler.PlayBaseSound(AudioType.SoundRocket);
            
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
            bullet1.Init(_gun1.transform.position, _fakeEnemy.transform, 0, true);
            bullet2.Init(_gun2.transform.position, _fakeEnemy.transform, 0, true);
            
            _audioHandler.PlayBaseSound(AudioType.SoundBullet);

            _nextFireTime = Time.time + _shotDelay;
        }

        private void FireRocketLauncher()
        {
            if (!_isActiveCombat
                || !_fakeEnemy
                || !_enemyAirDefence
                || _startLaunchBomb > Time.time
                || _isEmpty)
                return;

            Bullet bullet = _poolBullets.GetBullet(BulletType.FakeRocket);
            bullet.Init(_rocketLauncher.transform.position, _fakeEnemy.transform, 0, true);
            
            _audioHandler.PlayBaseSound(AudioType.SoundRocket);
            
            _isEmpty = true;
        }
    }
}
