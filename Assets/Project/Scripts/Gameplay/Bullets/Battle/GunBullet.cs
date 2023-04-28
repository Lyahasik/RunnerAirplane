using UnityEngine;

using RunnerAirplane.Core;
using RunnerAirplane.Gameplay.Objects;
using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Bullets.Battle
{
    public class GunBullet : Bullet
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speedMove;

        private Vector3 _direction;

        private bool _isActive;

        public override void Init(Vector3 position, Vector3 direction)
        {
            transform.position = position;
            transform.LookAt(position + direction);

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

        private void OnTriggerEnter(Collider other)
        {
            PlayerData playerData = other.GetComponent<PlayerData>();
            
            if (playerData)
            {
                playerData.CalculateNewHealth(MathOperationType.Subtraction, _damage);
                _poolBullets.ReturnBullet(this, BulletType.FakeBullet);
            }
            else if (other.GetComponent<BattleZone>())
            {
                _poolBullets.ReturnBullet(this, BulletType.FakeBullet);
            }
        }
    }
}
