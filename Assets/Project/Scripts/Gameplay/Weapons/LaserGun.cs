using UnityEngine;

using RunnerAirplane.Gameplay.Bullets;

namespace RunnerAirplane.Gameplay.Weapons
{
    public class LaserGun : Weapon
    {
        private Bullet _laser;
        
        public override bool IsActive
        {
            set
            {
                _isActive = value;
                
                if (_isActive)
                    Fire();
                else
                    OffLaser();
            }
        }

        private void Awake()
        {
            _isActive = false;
        }

        protected override void Update() 
        {
            if (Input.GetKeyDown(KeyCode.A))
                IsActive = !_isActive;
        }

        protected override void Fire()
        {
            if (_laser)
                return;

            _laser = _poolBullets.GetBullet(_bulletType);
            _laser.Init(_muzzle.transform.position, -transform.forward, _damage);
            _laser.transform.parent = transform;
        }

        private void OffLaser()
        {
            if (_laser)
                _poolBullets.ReturnBullet(_laser, BulletType.Laser);
            
            _laser = null;
        }
    }
}
