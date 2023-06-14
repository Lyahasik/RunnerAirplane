using UnityEngine;

using RunnerAirplane.Core;
using RunnerAirplane.Core.Pool;

namespace RunnerAirplane.Gameplay.Bullets
{
    public abstract class Bullet : MonoBehaviour
    {
        protected PoolBullets _poolBullets;
        public PoolBullets PoolBullets
        {
            set => _poolBullets = value;
        }

        public virtual void Init(Vector3 position, int damage = 0) {}
        public virtual void Init(Vector3 position, Transform targetTransform, int damage = 0, bool isPlayerWeapon = false) {}
        public virtual void Init(Vector3 position, Vector3 direction, int damage = 0, bool isPlayerWeapon = false) {}

        public virtual void Init(FiringZone firingZone, Vector3 position, Vector3 direction, float distance, int damage = 0) {}
        public virtual void Init(Vector3 position, Vector3 direction, Transform targetTransform,  int damage = 0) {}
        public virtual void Init(Vector3 position, Transform targetTransform, Vector3 direction, float distance, int damage = 0) {}

        public abstract void Reset(Vector3 newPosition);
    }
}
