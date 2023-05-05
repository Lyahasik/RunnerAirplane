using UnityEngine;

using RunnerAirplane.Core;
using RunnerAirplane.Gameplay.Objects;
using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Bullets.Battle
{
    public class HomingRocket : Bullet
    {
        
        private int _damage;
        [SerializeField] private float _speedTurn;
        [SerializeField] private float _speedMove;
        [SerializeField] private float _delayStartHoming;
        [SerializeField] private float _timeHoming;
        private float _startTimeHoming;
        private float _endTimeHoming;
        
        [SerializeField] private float _explosionScale;

        private Transform _targetTransform;

        private bool _isActive;
        private bool _isHoming;
        private bool _isHomingCompleted;

        public override void Init(Vector3 position, Vector3 direction, Transform targetTransform, int damage = 0)
        {
            transform.position = position;
            transform.rotation = Quaternion.LookRotation(direction);
            _targetTransform = targetTransform;
            
            _damage = damage;

            _isActive = true;

            _startTimeHoming = Time.time + _delayStartHoming;
        }

        private void Update()
        {
            TrySwitchHoming();
            Turn();
            Movement();
        }

        private void TrySwitchHoming()
        {
            if (!_isActive
                || _isHomingCompleted)
                return;
            
            if (_isHoming)
            {
                if (_endTimeHoming > Time.time)
                    return;

                _isHoming = false;
                _isHomingCompleted = true;
            }
            else
            {
                if (_startTimeHoming > Time.time)
                    return;
                
                _isHoming = true;
                _endTimeHoming = Time.time + _timeHoming;
            }
        }

        private void Turn()
        {
            if (!_isHoming
                || !_targetTransform)
                return;

            Vector3 direction = _targetTransform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _speedTurn * Time.deltaTime);
        }

        private void Movement()
        {
            if (!_isActive)
                return;
            
            transform.position += transform.forward * _speedMove * Time.deltaTime;
            
            if (!_targetTransform)
                _poolBullets.ReturnBullet(this, BulletType.HomingRocket);
        }

        public override void Reset(Vector3 newPosition)
        {
            _isActive = false;
            _isHoming = false;
            _isHomingCompleted = false;
            
            transform.position = newPosition;
        }
        
        private void Explosion()
        {
            Bullet bullet = _poolBullets.GetBullet(BulletType.Explosion);
            bullet.Init(transform.position);
            bullet.transform.localScale = new Vector3(_explosionScale, _explosionScale, _explosionScale);
                
            _poolBullets.ReturnBullet(this, BulletType.HomingRocket);
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerData playerData = other.GetComponent<PlayerData>();
            
            if (playerData)
            {
                playerData.CalculateNewHealth(MathOperationType.Subtraction, _damage);
                Explosion();
            }
            else if (other.GetComponent<BattleBorders>())
            {
                _poolBullets.ReturnBullet(this, BulletType.HomingRocket);
            }
        }
    }
}
