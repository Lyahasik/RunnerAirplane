using UnityEngine;

namespace RunnerAirplane.Gameplay.Bullets.Fake
{
    [RequireComponent(typeof(Rigidbody))]
    public class FakeFlyingBomb : Bullet
    {
        private const float _bottomLineDestruction = -10f;
        
        [SerializeField] private float _distanceExplosion;
        [SerializeField] private float _powerImpulse;
        
        private Rigidbody _rigidbody;

        private Transform _targetTransform;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public override void Init(Vector3 position, Transform targetTransform, int damage = 0)
        {
            transform.position = position;
            _targetTransform = targetTransform;
            
            _rigidbody.AddForce(transform.forward * _powerImpulse);
        }

        private void FixedUpdate()
        {
            if (!_targetTransform
                || Vector3.Distance(_rigidbody.transform.position, _targetTransform.position) <= _distanceExplosion
                || _rigidbody.transform.position.y < _bottomLineDestruction)
                Destroy(gameObject);
        }

        public override void Reset(Vector3 newPosition)
        {
            transform.position = newPosition;
        }
    }
}
