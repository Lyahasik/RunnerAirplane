using System.Collections.Generic;
using UnityEngine;

using RunnerAirplane.Gameplay.Weapons;
using RunnerAirplane.UI.Level;

namespace RunnerAirplane.Gameplay.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private List<MachineGun> _machineGuns;
        [SerializeField] private WorldLookAt _lookAtUI;
        [SerializeField] private RectTransform _rectTransformHealth;

        private bool _isActive;

        public bool IsActive
        {
            set
            {
                _isActive = value;
                
                if (_isActive)
                    Attack();
                else
                    Stop();
            }   
        }

        private void Attack()
        {
            foreach (MachineGun machineGun in _machineGuns)
            {
                machineGun.IsActive = true;
            }

            Vector2 size = new Vector2(200, 80);
            _rectTransformHealth.sizeDelta = size;
            _lookAtUI.IsBattle = true;
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
