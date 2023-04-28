using UnityEngine;

using RunnerAirplane.Core;
using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Bullets.Battle
{
    public class ExplosiveRocket : Bullet
    {
        private const float _explosionDistance = 0.5f;
        
        private int _damage;
        [SerializeField] private float _speedMove;
        [SerializeField] private Explosion _prefabExplosion;

        private Vector3 _direction;
        private Vector3 _targetPosition;

        private bool _isActive;

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
            Instantiate(_prefabExplosion, transform.position, Quaternion.identity).Init(_damage);
            _poolBullets.ReturnBullet(this, BulletType.ExplosiveRocket);
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
                Explosion();
            }
            else if (other.GetComponent<BattleZone>())
            {
                Explosion();
            }
        }
    }
}
