using UnityEngine;

using RunnerAirplane.Core;
using RunnerAirplane.Core.Pool;

namespace RunnerAirplane.Gameplay.Bullets
{
    public abstract class Bullet : MonoBehaviour, IPooling
    {
        protected PoolBullets _poolBullets;
        public PoolBullets PoolBullets
        {
            set => _poolBullets = value;
        }
        
        public abstract void Init(Vector3 position, Transform targetTransform);

        public abstract void Reset(Vector3 newPosition);
    }
}
