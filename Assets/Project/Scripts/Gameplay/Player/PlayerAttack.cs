using System.Collections.Generic;
using UnityEngine;

using RunnerAirplane.Gameplay.Weapons;

namespace RunnerAirplane.Gameplay.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private List<MachineGun> _machineGuns;

        private void Start()
        {
            Attack();
        }

        private void Attack()
        {
            foreach (MachineGun machineGun in _machineGuns)
            {
                machineGun.IsActive = true;
            }
        }

        private void Stop()
        {
            foreach (MachineGun machineGun in _machineGuns)
            {
                machineGun.IsActive = false;
            }
        }
    }
}
