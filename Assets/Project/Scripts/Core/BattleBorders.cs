using System.Collections.Generic;
using UnityEngine;

namespace RunnerAirplane.Core
{
    public class BattleBorders : MonoBehaviour
    {
        [SerializeField] private List<Collider> _colliders;

        private void OnEnable()
        {
            LevelHandler.OnStartBattle += ActiveColliders;
        }

        private void OnDisable()
        {
            LevelHandler.OnStartBattle -= ActiveColliders;
        }

        private void ActiveColliders()
        {
            foreach (Collider collider in _colliders)
            {
                collider.enabled = true;
            }
        }
    }
}
