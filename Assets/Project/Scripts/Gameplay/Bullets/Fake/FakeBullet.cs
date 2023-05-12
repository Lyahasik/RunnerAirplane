using UnityEngine;

using RunnerAirplane.Core;
using RunnerAirplane.Gameplay.Enemies;
using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Bullets.Fake
{
    public class FakeBullet : Bullet
    {
        [SerializeField] private float _distanceExplosion;
        [SerializeField] private float _explosionScale;
        [SerializeField] private float _speedMove;

        private Vector3 _direction;
        private Transform _targetTransform;

        private bool _isActive;
        private bool _isPlayerWeapon;

        public override void Init(Vector3 position, Transform targetTransform, int damage = 0, bool isPlayerWeapon = false)
        {
            transform.position = position;
            _targetTransform = targetTransform;
            transform.LookAt(_targetTransform.position);

            _isActive = true;
            _isPlayerWeapon = isPlayerWeapon;
        }

        private void Update()
        {
            if (LevelHandler.PauseGame)
                return;
            
            Movement();
        }

        private void Movement()
        {
            if (!_isActive)
                return;
            
            if (!_targetTransform)
                _poolBullets.ReturnBullet(this, BulletType.FakeBullet);
            
            transform.LookAt(_targetTransform);
            transform.position += transform.forward * _speedMove * Time.deltaTime;
        }

        public override void Reset(Vector3 newPosition)
        {
            _isActive = false;
            transform.position = newPosition;
        }
        
        private void Explosion()
        {
            Bullet bullet = _poolBullets.GetBullet(BulletType.ExplosionBullet);
            bullet.Init(transform.position);
            bullet.transform.localScale = new Vector3(_explosionScale, _explosionScale, _explosionScale);
                
            _poolBullets.ReturnBullet(this, BulletType.FakeBullet);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isPlayerWeapon)
            {
                if (other.GetComponent<FakeEnemy>())
                    Explosion();
            }
            else
            {
                if (other.GetComponent<PlayerData>())
                    Explosion();
            }
        }
    }
}
