using UnityEngine;

namespace RunnerAirplane.Gameplay.Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    public class FakeFlyingBomb : Bullet
    {
        [SerializeField] private float _distanceExplosion;
        [SerializeField] private float _powerImpulse;
        
        private Rigidbody _rigidbody;

        private Transform _targetTransform;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(_rigidbody.transform.position, _targetTransform.position) <= _distanceExplosion
                || _rigidbody.transform.position.z < -10f)
                Destroy(gameObject);
        }

        public override void Init(Vector3 position, Transform targetTransform)
        {
            transform.position = position;
            _targetTransform = targetTransform;
            
            _rigidbody.AddForce(transform.forward * _powerImpulse);
        }

        public override void Reset(Vector3 newPosition)
        {
            transform.position = newPosition;
        }
    }
}
