using UnityEngine;

using RunnerAirplane.Gameplay.Objects;
using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Bullets.Battle
{
    [RequireComponent(typeof(Collider))]
    public class Laser : Bullet
    {
        private int _damage;
        [SerializeField] private GameObject _warningEffect;
        [SerializeField] private GameObject _damageEffect;
        [SerializeField] private float _delayActivateDamage;
        [SerializeField] private float _delayMakeDamage;
        
        private float _nextMakeDamage;

        private PlayerData _playerData;
        
        private Collider _collider;
        
        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public override void Init(Vector3 position, Vector3 direction, int damage = 0)
        {
            transform.position = position;
            transform.rotation = Quaternion.LookRotation(direction);

            _damage = damage;
            Invoke(nameof(ActivateDamage), _delayActivateDamage);
        }

        private void ActivateDamage()
        {
            _collider.enabled = true;
            _warningEffect.SetActive(false);
            _damageEffect.SetActive(true);
        }

        private void Update()
        {
            MakeDamage();
        }

        private void MakeDamage()
        {
            if (!_playerData
                || _nextMakeDamage > Time.time)
                return;
            
            _playerData.CalculateNewHealth(MathOperationType.Subtraction, _damage);
            _nextMakeDamage = Time.time + _delayMakeDamage;
        }

        public override void Reset(Vector3 newPosition)
        {
            _collider.enabled = false;
            _warningEffect.SetActive(true);
            _damageEffect.SetActive(false);
            
            transform.parent = _poolBullets.transform;
            transform.position = newPosition;

            _playerData = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerData playerData = other.GetComponent<PlayerData>();

            if (playerData)
            {
                _playerData = playerData;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            PlayerData playerData = other.GetComponent<PlayerData>();

            if (playerData)
            {
                _playerData = null;
            }
        }
    }
}
