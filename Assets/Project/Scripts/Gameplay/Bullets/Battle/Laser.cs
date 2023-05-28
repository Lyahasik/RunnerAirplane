using System;
using UnityEngine;

using RunnerAirplane.Core;
using RunnerAirplane.Gameplay.Objects;
using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Bullets.Battle
{
    [RequireComponent(typeof(Collider))]
    public class Laser : Bullet
    {
        private int _damage;
        [SerializeField] private GameObject _base;
        [SerializeField] private Vector2 _scaleBase;
        [SerializeField] private Vector2 _scaleActive;
        
        [Space]
        [SerializeField] private ParticleSystem _visualEffect;
        [SerializeField] private float _delayActivateDamage;
        [SerializeField] private float _delayMakeDamage;
        
        private float _nextMakeDamage;

        private PlayerData _playerData;
        
        private Collider _collider;
        
        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public override void Init(Vector3 position, Vector3 direction, int damage = 0, bool isPlayerWeapon = false)
        {
            transform.position = position;
            transform.rotation = Quaternion.LookRotation(direction);

            _damage = damage;
            _base.transform.localScale = new Vector3(_scaleBase.x, _scaleBase.y, _base.transform.localScale.z);
            Invoke(nameof(ActivateDamage), _delayActivateDamage);
        }

        private void ActivateDamage()
        {
            _visualEffect.Play();
            _base.transform.localScale = new Vector3(_scaleActive.x, _scaleActive.y, _base.transform.localScale.z);
            
            _collider.enabled = true;
        }

        private void Update()
        {
            if (LevelHandler.PauseGame)
                return;
            
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
            
            transform.parent = _poolBullets.transform;
            transform.position = newPosition;
            
            _visualEffect.Stop();
            _visualEffect.Clear();

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
