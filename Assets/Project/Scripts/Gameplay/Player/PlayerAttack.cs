using System.Collections.Generic;
using UnityEngine;

using RunnerAirplane.Gameplay.Weapons;
using RunnerAirplane.UI.Level;
using RunnerAirplane.Core.Audio;

namespace RunnerAirplane.Gameplay.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private AudioHandler _audioHandler;
        
        [SerializeField] private List<MachineGun> _machineGuns;
        [SerializeField] private WorldLookAt _lookAtUI;
        [SerializeField] private RectTransform _rectTransformHealth;

        [SerializeField] private bool _isShooting = true;
        
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

        private void Start()
        {
            _audioHandler = FindObjectOfType<AudioHandler>();
            
            _machineGuns[0].Init(_audioHandler);
        }

        private void Attack()
        {
            if (!_isShooting)
                return;
            
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
