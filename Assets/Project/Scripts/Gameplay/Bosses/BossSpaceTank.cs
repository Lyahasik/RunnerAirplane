using System.Collections.Generic;
using UnityEngine;

using RunnerAirplane.Gameplay.Weapons;

namespace RunnerAirplane.Gameplay.Bosses
{
    public class BossSpaceTank : Boss
    {
        private bool _isMoving;
        [SerializeField] private Transform _targetMovementTransform;
        [SerializeField] private float _speedMovement;
        [SerializeField] private float _speedTurn;
        
        [Space]
        [SerializeField] private SpyingPlayer _spyingPlayer;
        [SerializeField] private float _rechargeAttack;
        [SerializeField] private float _delayStartAttack;
        
        [Space]
        [SerializeField] private List<MachineGun> _machineGuns;
        [SerializeField] private float _timeAttack1;
        private float _timeStartAttack1;
        private float _timeEndAttack1;
        private bool _isActiveAttack1;
        
        [Space]
        [SerializeField] private List<LaserGun> _laserGuns;
        [SerializeField] private float _timeAttack2;
        private float _timeStartAttack2;
        private float _timeEndAttack2;
        private bool _isActiveAttack2;

        private void Awake()
        {
            _timeStartAttack1 = Time.time + _delayStartAttack;
        }

        public override void StartBattle()
        {
            _isMoving = true;
        }

        public override void EndBattle()
        {
            foreach (MachineGun machineGun in _machineGuns)
            {
                machineGun.IsActive = false;
            }
            foreach (LaserGun laserGun in _laserGuns)
            {
                laserGun.IsActive = false;
            }
            
            _isMoving = false;
        }

        private void Update()
        {
            Movement();
            
            StartFire1();
            TryEndAttack1();
            
            StartFire2();
            TryEndAttack2();
        }

        private void Movement()
        {
            if (!_isMoving)
                return;
            
            Turn();
            float stepX = (_targetMovementTransform.position - transform.position).normalized.x;
            transform.position += Vector3.right * stepX * _speedMovement * Time.deltaTime;
        }

        private void Turn()
        {
            Vector3 direction = _targetMovementTransform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _speedTurn * Time.deltaTime);
        }

        private void StartFire1()
        {
            if (_isActiveAttack1
                || _isActiveAttack2
                || _timeStartAttack1 > Time.time
                || _timeStartAttack1 == 0f)
                return;

            foreach (MachineGun machineGun in _machineGuns)
            {
                machineGun.IsActive = true;
            }
            _isActiveAttack1 = true;
            _spyingPlayer.IsTurning = true;
            
            _timeEndAttack1 = Time.time + _timeAttack1;
        }

        private void TryEndAttack1()
        {
            if (!_isActiveAttack1
                || _timeEndAttack1 > Time.time)
                return;
            
            foreach (MachineGun machineGun in _machineGuns)
            {
                machineGun.IsActive = false;
            }
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

            foreach (LaserGun laserGun in _laserGuns)
            {
                laserGun.IsActive = true;
            }
            _isActiveAttack2 = true;
            _spyingPlayer.IsTurning = false;
            
            _timeEndAttack2 = Time.time + _timeAttack2;
        }
        
        private void TryEndAttack2()
        {
            if (!_isActiveAttack2
                || _timeEndAttack2 > Time.time)
                return;

            foreach (LaserGun laserGun in _laserGuns)
            {
                laserGun.IsActive = false;
            }
            _isActiveAttack2 = false;
            
            _timeStartAttack1 = Time.time + _rechargeAttack;
            _timeStartAttack2 = 0f;
        }
    }
}
