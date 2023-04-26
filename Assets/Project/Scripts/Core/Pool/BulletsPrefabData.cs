using System;

using RunnerAirplane.Gameplay.Bullets;

namespace RunnerAirplane.Core.Pool
{
    [Serializable]
    public class BulletsPrefabData
    {
        public BulletType Type;
        public Bullet Prefab;
    }
}
