using UnityEngine;

using RunnerAirplane.Core;
using RunnerAirplane.Gameplay.Objects;
using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Bullets.Battle
{
    public class Rocket : Bullet
    {
        private int _damage;
        [SerializeField] private float _speedMove;

        private bool _isActive;

        public override void Init(Vector3 position, Vector3 direction, int damage = 0, bool isPlayerWeapon = false)
        {
            transform.position = position;
            transform.rotation = Quaternion.LookRotation(direction);
            
            _damage = damage;

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
                _poolBullets.ReturnBullet(this, BulletType.Rocket);
            }
            else if (other.GetComponent<BattleBorders>())
            {
                _poolBullets.ReturnBullet(this, BulletType.Rocket);
            }
        }
    }
}
