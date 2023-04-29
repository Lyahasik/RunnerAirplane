using System.Collections.Generic;
using UnityEngine;

using RunnerAirplane.Gameplay.Weapons;

namespace RunnerAirplane.Gameplay.Bosses
{
    public class BossBattleship : MonoBehaviour
    {
        [SerializeField] private float _rechargeAttack;
        [SerializeField] private float _delayStartAttack;
        
        [Space]
        [SerializeField] private List<Shotgun> _shotguns;
        [SerializeField] private float _timeAttack1;
        private float _timeStartAttack1;
        private float _timeEndAttack1;
        private bool _isActiveAttack1;
        
        [Space]
        [SerializeField] private Bombardment _bombardment;
        [SerializeField] private float _timeAttack2;
        private float _timeStartAttack2;
        private float _timeEndAttack2;
        private bool _isActiveAttack2;

        private void Awake()
        {
            _timeStartAttack1 = Time.time + _delayStartAttack;
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

            foreach (Shotgun shotgun in _shotguns)
            {
                shotgun.IsActive = true;
            }
            _isActiveAttack1 = true;
            
            _timeEndAttack1 = Time.time + _timeAttack1;
        }

        private void TryEndAttack1()
        {
            if (!_isActiveAttack1
                || _timeEndAttack1 > Time.time)
                return;
            
            foreach (Shotgun shotgun in _shotguns)
            {
                shotgun.IsActive = false;
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
            
            _bombardment.IsActive = true;
            _isActiveAttack2 = true;
            
            _timeEndAttack2 = Time.time + _timeAttack2;
        }
        
        private void TryEndAttack2()
        {
            if (!_isActiveAttack2
                || _timeEndAttack2 > Time.time)
                return;
            
            _bombardment.IsActive = false;
            _isActiveAttack2 = false;
            
            _timeStartAttack1 = Time.time + _rechargeAttack;
            _timeStartAttack2 = 0f;
        }
    }
}
