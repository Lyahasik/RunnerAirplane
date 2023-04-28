using UnityEngine;

using RunnerAirplane.Core.Pool;

namespace RunnerAirplane.Gameplay.Bullets
{
    public abstract class Bullet : MonoBehaviour, IBulletPooling
    {
        protected PoolBullets _poolBullets;
        public PoolBullets PoolBullets
        {
            set => _poolBullets = value;
        }

        public virtual void Init(Vector3 position, Transform targetTransform) {}
        public virtual void Init(Vector3 position, Vector3 direction) {}
        public virtual void Init(Vector3 position, Vector3 direction, float distance) {}
        public virtual void Init(Vector3 position, Transform targetTransform, Vector3 direction) {}
        public virtual void Init(Vector3 position, Transform targetTransform, Vector3 direction, float distance) {}

        public abstract void Reset(Vector3 newPosition);
    }
}
