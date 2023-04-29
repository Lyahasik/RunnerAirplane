using UnityEngine;

using RunnerAirplane.Gameplay.Weapons;

namespace RunnerAirplane
{
    public class BossSubmarine : MonoBehaviour
    {
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
            _timeStartAttack = Time.time + _delayStartAttack;
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
