using UnityEngine;

using RunnerAirplane.Core;
using RunnerAirplane.Gameplay.Objects;
using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Bullets.Battle
{
    public class ExplosiveBullet : Bullet
    {
        private const float _explosionDistance = 0.5f;
        
        private int _damage;
        [SerializeField] private float _speedMove;

        private Vector3 _direction;
        private Vector3 _targetPosition;

        private Vector3 _directionMiniBullet1;
        private Vector3 _directionMiniBullet2;
        private Vector3 _directionMiniBullet3;
        private Vector3 _directionMiniBullet4;

        private bool _isActive;

        private void Awake()
        {
            _directionMiniBullet1 = new Vector3(1f, 0f, 1f).normalized;
            _directionMiniBullet2 = new Vector3(-1f, 0f, 1f).normalized;
            _directionMiniBullet3 = new Vector3(1f, 0f, -1f).normalized;
            _directionMiniBullet4 = new Vector3(-1f, 0f, -1f).normalized;
        }

        public override void Init(Vector3 position, Vector3 direction, float distance, int damage = 0)
        {
            _damage = damage;
            transform.position = position;
            transform.LookAt(position + direction);

            _targetPosition = position + transform.forward * distance;

            _isActive = true;
        }

        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            if (!_isActive)
                return;
            
            transform.position += transform.forward * _speedMove * Time.deltaTime;
            
            if (Vector3.Distance(transform.position, _targetPosition) < _explosionDistance)
                Explosion();
        }

        private void Explosion()
        {
            int damage = _damage / 4;
            _poolBullets
                .GetBullet(BulletType.GunBullet)
                .Init(transform.position, _directionMiniBullet1, damage);
            _poolBullets
                .GetBullet(BulletType.GunBullet)
                .Init(transform.position, _directionMiniBullet2, damage);
            _poolBullets
                .GetBullet(BulletType.GunBullet)
                .Init(transform.position, _directionMiniBullet3, damage);
            _poolBullets
                .GetBullet(BulletType.GunBullet)
                .Init(transform.position, _directionMiniBullet4, damage);
            
            _poolBullets.ReturnBullet(this, BulletType.ExplosiveBullet);
        }

        public override void Reset(Vector3 newPosition)
        {
            _isActive = false;
            transform.position = newPosition;
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerData playerData = other.GetComponent<PlayerData>();
            
            if (playerData)
            {
                playerData.CalculateNewHealth(MathOperationType.Subtraction, _damage);
                _poolBullets.ReturnBullet(this, BulletType.ExplosiveBullet);
            }
            else if (other.GetComponent<BattleZone>())
            {
                Explosion();
            }
        }
    }
}
