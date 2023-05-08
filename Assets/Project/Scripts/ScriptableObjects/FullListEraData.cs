using System.Collections.Generic;
using UnityEngine;

namespace RunnerAirplane.ScriptableObjects
{
    [CreateAssetMenu(fileName = "FullListEra", menuName = "Full list era", order = 51)]
    public class FullListEraData : ScriptableObject
    {
        [SerializeField] private List<ListEraData> _listsEra;

        public List<ListEraData> ListsEra => _listsEra;
    }
}
