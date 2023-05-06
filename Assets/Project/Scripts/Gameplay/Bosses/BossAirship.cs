using UnityEngine;

using RunnerAirplane.Gameplay.Weapons;

namespace RunnerAirplane.Gameplay.Bosses
{
    public class BossAirship : Boss
    {
        private MovementToPoint _movementToPoint;
        [SerializeField] private float _rechargeAttack;
        [SerializeField] private float _delayStartAttack;
        
        [Space]
        [SerializeField] private MachineGun _machineGun;
        [SerializeField] private float _timeAttack1;
        private float _timeStartAttack1;
        private float _timeEndAttack1;
        private bool _isActiveAttack1;
        
        [Space]
        [SerializeField] private RocketLauncher _rocketLauncher;
        [SerializeField] private float _timeAttack2;
        private float _timeStartAttack2;
        private float _timeEndAttack2;
        private bool _isActiveAttack2;

        private void Awake()
        {
            _movementToPoint = GetComponent<MovementToPoint>();
            _timeStartAttack1 = Time.time + _delayStartAttack;
        }

        public override void StartBattle()
        {
            _movementToPoint.enabled = true;
        }

        public override void EndBattle()
        {
            _machineGun.IsActive = false;
            _rocketLauncher.IsActive = false;
            
            _movementToPoint.enabled = false;
        }

        private void Update()
        {
            StartFire1();
            TryEndAttack1();
            
            StartFire2();
            TryEndAttack2();
        }

        private void StartFire1()
        {
            if (_isActiveAttack1
                || _isActiveAttack2
                || _timeStartAttack1 > Time.time
                || _timeStartAttack1 == 0f)
                return;
            
            _machineGun.IsActive = true;
            _isActiveAttack1 = true;
            
            _timeEndAttack1 = Time.time + _timeAttack1;
        }

        private void TryEndAttack1()
        {
            if (!_isActiveAttack1
                || _timeEndAttack1 > Time.time)
                return;
            
            _machineGun.IsActive = false;
            _isActiveAttack1 = false;

            _timeStartAttack1 = 0f;
            _timeStartAttack2 = Time.time + _rechargeAttack;
        }

        private void StartFire2()
        {
            if (_isActiveAttack1
                || _isActiveAttack2
                || _timeStartAttack2 > Time.time
                || _timeStartAttack2 == 0f)
                return;
            
            _rocketLauncher.IsActive = true;
            _isActiveAttack2 = true;
            
            _timeEndAttack2 = Time.time + _timeAttack2;
        }
        
        private void TryEndAttack2()
        {
            if (!_isActiveAttack2
                || _timeEndAttack2 > Time.time)
                return;
            
            _rocketLauncher.IsActive = false;
            _isActiveAttack2 = false;
            
            _timeStartAttack1 = Time.time + _rechargeAttack;
            _timeStartAttack2 = 0f;
        }
    }
}
