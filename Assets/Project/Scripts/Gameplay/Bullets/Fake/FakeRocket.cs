using UnityEngine;

namespace RunnerAirplane.Gameplay.Bullets.Fake
{
    public class FakeRocket : Bullet
    {
        [SerializeField] private float _distanceExplosion;
        [SerializeField] private float _speedMove;

        private Vector3 _direction;
        private Transform _targetTransform;

        private bool _isActive;

        public override void Init(Vector3 position, Transform targetTransform, int damage = 0)
        {
            transform.position = position;
            _targetTransform = targetTransform;
            transform.LookAt(_targetTransform.position);

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
            
            if (!_targetTransform
                || Vector3.Distance(transform.position, _targetTransform.position) < _distanceExplosion)
                _poolBullets.ReturnBullet(this, BulletType.FakeRocket);
        }

        public override void Reset(Vector3 newPosition)
        {
            _isActive = false;
            transform.position = newPosition;
        }
    }
}
