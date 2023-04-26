using System.Collections.Generic;
using UnityEngine;

using RunnerAirplane.Gameplay.Enemies;
using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Core
{
    public class FakeCombatHandler : MonoBehaviour
    {
        [SerializeField] private float _combatDistance;
        
        [SerializeField] private PlayerFakeCombat _playerFakeCombat;
        [SerializeField] private List<Enemy> _enemies;

        private void Update()
        {
            TryStartCombat();
        }

        private void TryStartCombat()
        {
            if (_playerFakeCombat.IsActiveCombat)
                return;
            
            foreach (Enemy enemy in _enemies)
            {
                if (!enemy
                    || enemy.IsActiveCombat)
                    continue;
                
                if (Vector3.Distance(_playerFakeCombat.transform.position, enemy.transform.position) < _combatDistance)
                {
                    if (_playerFakeCombat.TryStartCombat(enemy))
                    {
                        enemy.StartCombat(_playerFakeCombat);
                    }
                }
            }
        }
    }
}
