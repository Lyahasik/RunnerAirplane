using UnityEngine;

namespace RunnerAirplane.Core
{
    public interface IPooling
    {
        public void Init(Vector3 position, Transform targetTransform);

        public void Reset(Vector3 newPosition);
    }
}
