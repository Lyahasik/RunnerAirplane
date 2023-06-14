using System;
using RunnerAirplane.Gameplay;
using UnityEngine;

namespace RunnerAirplane.ScriptableObjects
{
    [Serializable]
    public class EraData
    {
        public int MinValue;
        public int MaxValue;
        public Technique Prefab;
    }
}
