using UnityEngine;

using RunnerAirplane.Core;
using RunnerAirplane.Gameplay.Bosses;
using RunnerAirplane.Gameplay.Objects;
using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Bullets.Battle
{
    public class GunBullet : Bullet
    {
        private int _damage;
        [SerializeField] private float _speedMove;
        [SerializeField] private float _explosionScale;

        private Vector3 _direction;

        private bool _isActive;
        private bool _isPlayerWeapon;

        public override void Init(Vector3 position, Vector3 direction, int damage = 0, bool isPlayerWeapon = false)
        {
            transform.position = position;
            transform.LookAt(position + direction);
            
            _damage = damage;
            _isPlayerWeapon = isPlayerWeapon;

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
        }

        public override void Reset(Vector3 newPosition)
        {
            _isActive = false;
            transform.position = newPosition;
        }

        private void CheckCollision(Collider other)
        {
            if (_isPlayerWeapon)
            {
                BossData bossData = other.GetComponent<BossData>();
            
                if (bossData)
                {
                    bossData.CalculateNewHealth(_damage);
                    Explosion();
                }
            }
            else
            {
                PlayerData playerData = other.GetComponent<PlayerData>();
            
                if (playerData)
                {
                    playerData.CalculateNewHealth(MathOperationType.Subtraction, _damage);
                    Explosion();
                }
            }
        }

        private void Explosion()
        {
            Bullet bullet = _poolBullets.GetBullet(BulletType.Explosion);
            bullet.Init(transform.position);
            bullet.transform.localScale = new Vector3(_explosionScale, _explosionScale, _explosionScale);
                
            _poolBullets.ReturnBullet(this, BulletType.GunBullet);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<BattleBorders>())
            {
                _poolBullets.ReturnBullet(this, BulletType.GunBullet);
            }
            else
            {
                CheckCollision(other);
            }
        }
    }
}
