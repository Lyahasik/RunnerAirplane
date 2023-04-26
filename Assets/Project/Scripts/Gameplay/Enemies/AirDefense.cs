using UnityEngine;

using RunnerAirplane.Gameplay.Bullets;

namespace RunnerAirplane.Gameplay.Enemies
{
    public class AirDefense : Enemy
    {
        [SerializeField] private float _fireDistance;
        [SerializeField] private GameObject _weapon;

        private bool _isEmpty;

        private void Awake()
        {
            _bulletType = BulletType.FakeRocket;
        }
        
        protected override void Update()
        {
            Fire();
            
            base.Update();
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

            _isEmpty = true;
        }
    }
}
