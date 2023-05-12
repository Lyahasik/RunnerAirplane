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
        [SerializeField] private float _explosionScale;

        private FiringZone _firingZone;
        private Vector3 _direction;
        private Vector3 _targetPosition;

        private bool _isActive;
        private bool _isInsideArea;

        public override void Init(FiringZone firingZone, Vector3 position, Vector3 direction, float distance, int damage = 0)
        {
            _firingZone = firingZone;
            
            transform.position = position;
            transform.LookAt(position + direction);
            _targetPosition = position + transform.forward * distance;
            
            _damage = damage;

            _isActive = true;
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
            
            transform.position += transform.forward * _speedMove * Time.deltaTime;

            if (_firingZone.IsInsideArea(transform.position))
                _isInsideArea = true;
            
            if (Vector3.Distance(transform.position, _targetPosition) < _explosionDistance
                || _isInsideArea && !_firingZone.IsInsideArea(transform.position))
                Explosion();
        }

        private void Explosion()
        {
            Bullet bullet = _poolBullets.GetBullet(BulletType.ExplosionBullet);
            bullet.Init(transform.position, _damage);
            bullet.transform.localScale = new Vector3(_explosionScale, _explosionScale, _explosionScale);
                
            _poolBullets.ReturnBullet(this, BulletType.ExplosiveRocket);
        }

        public override void Reset(Vector3 newPosition)
        {
            _isActive = false;
            _isInsideArea = false;
            transform.position = newPosition;
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerData playerData = other.GetComponent<PlayerData>();
            
            if (playerData)
            {
                Explosion();
            }
            else if (other.GetComponent<BattleBorders>())
            {
                Explosion();
            }
        }
    }
}
