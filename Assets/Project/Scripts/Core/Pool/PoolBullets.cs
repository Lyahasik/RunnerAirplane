using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using RunnerAirplane.Gameplay.Bullets;

namespace RunnerAirplane.Core.Pool
{
    public class PoolBullets : MonoBehaviour
    {
        [SerializeField] private List<BulletsPrefabData> _bulletsPrefabs;

        private Dictionary<BulletType, Stack<Bullet>> _bullets;

        private void Awake()
        {
            InitBullets();
        }

        private void InitBullets()
        {
            _bullets = new Dictionary<BulletType, Stack<Bullet>>();
            foreach (BulletType type in Enum.GetValues(typeof(BulletType)))
            {
                _bullets.Add(type, new Stack<Bullet>());
            }
        }

        public Bullet GetBullet(BulletType bulletType)
        {
            if (_bullets[bulletType].Count == 0)
            {
                Bullet bullet = Instantiate(_bulletsPrefabs.First(data => data.Type == bulletType).Prefab);
                bullet.PoolBullets = this;
                bullet.transform.parent = transform;
                
                return bullet;
            }

            return _bullets[bulletType].Pop();
        }

        public void ReturnBullet(Bullet bullet, BulletType bulletType)
        {
            bullet.Reset(transform.position);
            _bullets[bulletType].Push(bullet);
        }
    }
}
