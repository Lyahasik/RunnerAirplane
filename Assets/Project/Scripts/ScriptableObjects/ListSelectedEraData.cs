using System.Collections.Generic;
using UnityEngine;

namespace RunnerAirplane.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ListEra", menuName = "List era", order = 51)]
    public class ListSelectedEraData : ScriptableObject
    {
        [SerializeField] private List<EraData> _listEra;

        public List<EraData> ListEra => _listEra;
    }
}
