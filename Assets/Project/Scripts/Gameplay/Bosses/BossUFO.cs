using System.Collections.Generic;
using UnityEngine;

using RunnerAirplane.Gameplay.Weapons;

namespace RunnerAirplane.Gameplay.Bosses
{
    public class BossUFO : MonoBehaviour
    {
        [SerializeField] private float _rechargeAttack;
        [SerializeField] private float _delayStartAttack;
        
        [Space]
        [SerializeField] private List<LaserGun> _laserGuns;
        [SerializeField] private float _timeAttack;
        private float _timeStartAttack;
        private float _timeEndAttack;
        private bool _isActiveAttack;

        private void Awake()
        {
            _timeStartAttack = Time.time + _delayStartAttack;
        }

        private void Update()
        {
            StartFire();
            TryEndAttack();
        }

        private void StartFire()
        {
            if (_isActiveAttack
                || _timeStartAttack > Time.time)
                return;

            foreach (LaserGun laserGun in _laserGuns)
            {
                laserGun.IsActive = true;
            }
            _isActiveAttack = true;
            
            _timeEndAttack = Time.time + _timeAttack;
        }

        private void TryEndAttack()
        {
            if (!_isActiveAttack
                || _timeEndAttack > Time.time)
                return;

            foreach (LaserGun laserGun in _laserGuns)
            {
                laserGun.IsActive = false;
            }
            _isActiveAttack = false;

            _timeStartAttack = Time.time + _rechargeAttack;
        }
    }
}
