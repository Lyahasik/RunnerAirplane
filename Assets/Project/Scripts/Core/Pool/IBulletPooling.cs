using UnityEngine;

namespace RunnerAirplane.Core.Pool
{
    public interface IBulletPooling
    {
        public void Init(Vector3 position, Transform targetTransform);
        public void Init(Vector3 position, Vector3 direction);
        public void Init(Vector3 position, Vector3 direction, float distance);

        public void Init(Vector3 position, Transform targetTransform, Vector3 direction);

        public void Init(Vector3 position, Transform targetTransform, Vector3 direction, float distance);

        public void Reset(Vector3 newPosition);
    }
}
