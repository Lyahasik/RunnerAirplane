using UnityEngine;

namespace RunnerAirplane.Core.Pool
{
    public interface IBulletPooling
    {
        public void Init(Vector3 position, Transform targetTransform, int damage = 0);
        public void Init(Vector3 position, Vector3 direction, int damage = 0);
        public void Init(Vector3 position, Vector3 direction, float distance, int damage = 0);

        public void Init(Vector3 position, Transform targetTransform, Vector3 direction, int damage = 0);

        public void Init(Vector3 position, Transform targetTransform, Vector3 direction, float distance, int damage = 0);

        public void Reset(Vector3 newPosition);
    }
}
