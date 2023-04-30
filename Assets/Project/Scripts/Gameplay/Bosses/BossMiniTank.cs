using UnityEngine;

using RunnerAirplane.Gameplay.Weapons;

namespace RunnerAirplane
{
    public class BossMiniTank : MonoBehaviour
    {
        [SerializeField] private float _rechargeAttack;
        [SerializeField] private float _delayStartAttack;
        
        [Space]
        [SerializeField] private HomingRocketLauncher _homingRocketLauncher;
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
        
        [Space]
        [SerializeField] private LaserGun _laserGun;
        [SerializeField] private float _timeAttack3;
        private float _timeStartAttack3;
        private float _timeEndAttack3;
        private bool _isActiveAttack3;

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
            
            StartFire3();
            TryEndAttack3();
        }

        private void StartFire1()
        {
            if (_isActiveAttack1
                || _isActiveAttack2
                || _isActiveAttack3
                || _timeStartAttack1 > Time.time
                || _timeStartAttack1 == 0f)
                return;

            _homingRocketLauncher.IsActive = true;
            _isActiveAttack1 = true;
            
            _timeEndAttack1 = Time.time + _timeAttack1;
        }

        private void TryEndAttack1()
        {
            if (!_isActiveAttack1
                || _timeEndAttack1 > Time.time)
                return;
            
            _homingRocketLauncher.IsActive = false;
            _isActiveAttack1 = false;

            _timeStartAttack1 = 0f;
            _timeStartAttack2 = Time.time + _rechargeAttack;
        }

        private void StartFire2()
        {
            if (_isActiveAttack1
                || _isActiveAttack2
                || _isActiveAttack3
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
            
            _timeStartAttack2 = 0f;
            _timeStartAttack3 = Time.time + _rechargeAttack;
        }

        private void StartFire3()
        {
            if (_isActiveAttack1
                || _isActiveAttack2
                || _isActiveAttack3
                || _timeStartAttack3 > Time.time
                || _timeStartAttack3 == 0f)
                return;
            
            _laserGun.IsActive = true;
            _isActiveAttack3 = true;
            
            _timeEndAttack3 = Time.time + _timeAttack3;
        }
        
        private void TryEndAttack3()
        {
            if (!_isActiveAttack3
                || _timeEndAttack3 > Time.time)
                return;

            _laserGun.IsActive = false;
            _isActiveAttack3 = false;
            
            _timeStartAttack3 = 0f;
            _timeStartAttack1 = Time.time + _rechargeAttack;
        }
    }
}
