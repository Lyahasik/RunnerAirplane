using UnityEngine;

using RunnerAirplane.Core.Pool;
using RunnerAirplane.Gameplay.Bullets;
using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected PoolBullets _poolBullets;
        protected BulletType _bulletType;
        
        protected PlayerFakeCombat _playerFakeCombat;

        [SerializeField] private float _combatEndDistance;
        [SerializeField] private float _delayStartFire;
        protected bool _isActiveCombat;
        protected float _startFireTime;
        
        public bool IsActiveCombat => _isActiveCombat;

        protected virtual void Update()
        {
            TryEndCombat();
        }

        public void StartCombat(PlayerFakeCombat playerFakeCombat)
        {
            _playerFakeCombat = playerFakeCombat;
            _startFireTime = Time.time + _delayStartFire;
            _isActiveCombat = true;
        }

        private void TryEndCombat()
        {
            if (_isActiveCombat
                && transform.position.z - _playerFakeCombat.transform.position.z <= _combatEndDistance)
            {
                _playerFakeCombat.EndCombat();
                Destroy(gameObject);
            }
        }
    }
}
