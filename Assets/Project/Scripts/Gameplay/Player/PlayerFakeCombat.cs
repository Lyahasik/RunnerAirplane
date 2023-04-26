using UnityEngine;

using RunnerAirplane.Core.Pool;
using RunnerAirplane.Gameplay.Bullets;
using RunnerAirplane.Gameplay.Enemies;

namespace RunnerAirplane.Gameplay.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerFakeCombat : MonoBehaviour
    {
        [SerializeField] private PoolBullets _poolBullets;
        
        [Space]
        [SerializeField] private GameObject _gun1;
        [SerializeField] private GameObject _gun2;
        [SerializeField] private float _shotDelay;
        
        [Space]
        [SerializeField] private GameObject _bombLauncher;
        [SerializeField] private float _delayLaunchBomb;

        private PlayerMovement _playerMovement;
        private Enemy _enemy;
        private bool _enemyHelicopter;
        private bool _enemyAirDefence;

        private bool _isActiveCombat;
        private float _timeEndCombat;
        private float _nextFireTime;

        private float _startLaunchBomb;
        
        private bool _isEmpty;

        public bool IsActiveCombat => _isActiveCombat;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            FireGun();
            FireBombLauncher();
        }

        public bool TryStartCombat(Enemy enemy)
        {
            _enemy = enemy;
            if (_enemy is Helicopter)
                _enemyHelicopter = true;
            if (_enemy is AirDefense)
            {
                _enemyAirDefence = true;
                _startLaunchBomb = Time.time + _delayLaunchBomb;
            }
            
            _isActiveCombat = true;
            _playerMovement.IsCombating = true;

            return true;
        }

        public void EndCombat()
        {
            _enemy = null;
            _enemyHelicopter = false;
            _enemyAirDefence = false;
            
            _isActiveCombat = false;
            _isEmpty = false;
            _playerMovement.IsCombating = false;
        }

        private void FireGun()
        {
            if (!_isActiveCombat
                || !_enemy
                || !_enemyHelicopter
                || _nextFireTime > Time.time)
                return;

            Bullet bullet1 = _poolBullets.GetBullet(BulletType.FakeBullet);
            Bullet bullet2 = _poolBullets.GetBullet(BulletType.FakeBullet);
            bullet1.Init(_gun1.transform.position, _enemy.transform);
            bullet2.Init(_gun2.transform.position, _enemy.transform);

            _nextFireTime = Time.time + _shotDelay;
        }

        private void FireBombLauncher()
        {
            if (!_isActiveCombat
                || !_enemy
                || !_enemyAirDefence
                || _startLaunchBomb > Time.time
                || _isEmpty)
                return;

            Bullet bullet = _poolBullets.GetBullet(BulletType.FakeFlyingBomb);
            bullet.Init(_bombLauncher.transform.position, _enemy.transform);
            Debug.Log(_isEmpty);
            
            _isEmpty = true;
        }
    }
}
