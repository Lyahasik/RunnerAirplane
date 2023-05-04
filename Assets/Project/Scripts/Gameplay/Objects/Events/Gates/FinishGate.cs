using System;
using UnityEngine;

using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Objects.Events.Gates
{
    public class FinishGate : MonoBehaviour
    {
        public static event Action OnFinish;

        private void OnTriggerExit(Collider other)
        {
            PlayerData playerData = other.GetComponent<PlayerData>();

            if (playerData)
            {
                OnFinish?.Invoke();
            }
        }
    }
}
