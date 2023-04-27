using UnityEngine;

using RunnerAirplane.Gameplay.Bullets;

namespace RunnerAirplane.Gameplay.Enemies
{
    public class AirDefense : FakeEnemy
    {
        [SerializeField] private float _fireDistance;
        [SerializeField] private GameObject _weapon;
        [SerializeField] private float _delayDamage;

        private bool _isEmpty;

        protected override void Awake()
        {
            base.Awake();
            
            _bulletType = BulletType.FakeRocket;
        }
        
        protected override void Update()
        {
            if (!_playerFakeCombat)
                return;
            
            base.Update();

            UpdateHealth();
            Fire();
        }
        
        private void UpdateHealth()
        {
            if (!_isActiveCombat
                || _distanceTraveled < 0.98f)
                return;
            
            SetHealth(0);
        }

        private void Fire()
        {
            if (!_isActiveCombat
                || _startFireTime > Time.time
                || _isEmpty
                || _fireDistance > Vector3.Distance(transform.position , _playerFakeCombat.transform.position))
                return;

            Bullet bullet = _poolBullets.GetBullet(BulletType.FakeRocket);
            bullet.Init(_weapon.transform.position, _playerFakeCombat.transform);
            Invoke(nameof(MakeDamage), _delayDamage);

            _isEmpty = true;
        }

        private void MakeDamage()
        {
            if (!_playerFakeCombat)
                return;
            
            _playerFakeCombat.TemporaryHealth = _health;
        }
    }
}
