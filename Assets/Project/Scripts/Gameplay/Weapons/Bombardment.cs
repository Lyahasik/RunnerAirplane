using UnityEngine;

using RunnerAirplane.Core;
using RunnerAirplane.Gameplay.Bullets;

namespace RunnerAirplane.Gameplay.Weapons
{
    public class Bombardment : Weapon
    {
        [SerializeField] private int _volleyAmount;
        [SerializeField] private FiringZone _firingZone;
        protected override void Fire()
        {
            if (!_isActive
                || _nextFireTime > Time.time)
                return;

            Volley();

            _nextFireTime = Time.time + _delayFire;
        }

        private void Volley()
        {
            for (int i = 0; i < _volleyAmount; i++)
            {
                Bullet bullet = _poolBullets.GetBullet(_bulletType);
                Vector3 position = _firingZone.GetRandomPointInArea();
                bullet.Init(position, _damage);
            }
        }
    }
}
