using UnityEngine;

using RunnerAirplane.Gameplay.Weapons;

namespace RunnerAirplane.Gameplay.Bosses
{
    public class BossSubmarine : Boss
    {
        private MovementToPoint _movementToPoint;
        [SerializeField] private float _rechargeAttack;
        [SerializeField] private float _delayStartAttack;
        
        [Space]
        [SerializeField] private Bombardment _bombardment;
        [SerializeField] private float _timeAttack;
        private float _timeStartAttack;
        private float _timeEndAttack;
        private bool _isActiveAttack;

        private void Awake()
        {
            _movementToPoint = GetComponent<MovementToPoint>();
            _timeStartAttack = Time.time + _delayStartAttack;
        }

        public override void StartBattle()
        {
            _movementToPoint.enabled = true;
        }

        public override void EndBattle()
        {
            _bombardment.IsActive = false;
            
            _movementToPoint.enabled = false;
        }

        private void Update()
        {
            StartFire1();
            TryEndAttack1();
        }

        private void StartFire1()
        {
            if (_isActiveAttack
                || _timeStartAttack > Time.time)
                return;
            
            _bombardment.IsActive = true;
            _isActiveAttack = true;
            
            _timeEndAttack = Time.time + _timeAttack;
        }

        private void TryEndAttack1()
        {
            if (!_isActiveAttack
                || _timeEndAttack > Time.time)
                return;
            
            _bombardment.IsActive = false;
            _isActiveAttack = false;

            _timeStartAttack = Time.time + _rechargeAttack;
        }
    }
}
