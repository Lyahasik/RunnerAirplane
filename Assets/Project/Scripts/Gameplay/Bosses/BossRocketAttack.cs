using System.Collections.Generic;
using UnityEngine;

using RunnerAirplane.Gameplay.Weapons;

namespace RunnerAirplane.Gameplay.Bosses
{
    public class BossRocketAttack : Boss
    {
        [SerializeField] private float _timeLife;
        private float _timeEndLife;
        [SerializeField] private float _rechargeAttack;
        [SerializeField] private float _delayStartAttack;

        [Space]
        [SerializeField] private List<RocketLauncher> _rocketLaunchers;
        [SerializeField] private float _timeAttack;
        [Range(1, 100)] [SerializeField] private int _probability;
        [SerializeField] private int _numberLaunches;
        
        private float _timeStartAttack;
        private float _timeEndAttack;
        private bool _isActiveAttack;

        private void Awake()
        {
            _timeStartAttack = Time.time + _delayStartAttack;
        }

        public override void StartBattle()
        {
            _timeEndLife = Time.time + _timeLife;
        }

        public override void EndBattle()
        {
            foreach (RocketLauncher rocketLauncher in _rocketLaunchers)
            {
                rocketLauncher.IsActive = false;
            }
        }

        private void Update()
        {
            StartFire();
            TryEndAttack();

            TryFinishLife();
        }

        private void StartFire()
        {
            if (_isActiveAttack
                || _timeStartAttack > Time.time)
                return;

            int number = 0;
            foreach (RocketLauncher rocketLauncher in _rocketLaunchers)
            {
                if (number < _numberLaunches
                    && Random.Range(1, 101) <= _probability)
                {
                    rocketLauncher.IsActive = true;
                    number++;
                }
            }
            _isActiveAttack = true;
            
            _timeEndAttack = Time.time + _timeAttack;
        }

        private void TryEndAttack()
        {
            if (!_isActiveAttack
                || _timeEndAttack > Time.time)
                return;

            foreach (RocketLauncher rocketLauncher in _rocketLaunchers)
            {
                rocketLauncher.IsActive = false;
            }
            _isActiveAttack = false;

            _timeStartAttack = Time.time + _rechargeAttack;
        }

        private void TryFinishLife()
        {
            if (_timeEndLife <= Time.time)
                Destroy(gameObject);
        }
    }
}
