using RunnerAirplane.Gameplay.Player;
using UnityEngine;

namespace RunnerAirplane.Gameplay.Objects.Obstacles
{
    public class DestroyingObstacle : StatChanger
    {
        private void Awake()
        {
            _operationType = MathOperationType.Subtraction;
        }
        
        protected override void InitText() {}

        public override void ProcessingPlayerData(PlayerData playerData)
        {
            if (_isLocked)
                return;
            
            playerData.CalculateNewHealth(_operationType, playerData.CurrentHealth);
            _isLocked = true;
        }
        
        protected override void OnTriggerEnter(Collider other)
        {
            PlayerData playerData = other.GetComponent<PlayerData>();

            if (playerData)
            {
                ProcessingPlayerData(playerData);
            }
        }
    }
}
