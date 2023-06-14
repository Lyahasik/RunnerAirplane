using System;
using System.Collections.Generic;
using RunnerAirplane.Gameplay;
using UnityEngine;

namespace RunnerAirplane.ScriptableObjects
{
    [Serializable]
    public class ListEraData
    {
        public int Number;
        public List<Technique> ListPrefabs;
    }
}
