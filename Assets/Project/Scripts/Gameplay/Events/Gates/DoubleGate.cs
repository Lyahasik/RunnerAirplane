using UnityEngine;

using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Events.Gates
{
    public class DoubleGate : MonoBehaviour
    {
        [SerializeField] private Gate _leftGate;
        [SerializeField] private Gate _rightGate;

        private bool _isLocked;

        private void ProcessingCollision(Vector3 point, PlayerData playerData)
        {
            if (point.x < 0f)
            {
                _leftGate.ProcessingPlayerData(playerData);
            }
            else
            {
                _rightGate.ProcessingPlayerData(playerData);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isLocked)
                return;
            
            PlayerData playerData = other.GetComponent<PlayerData>();

            if (playerData)
            {
                ProcessingCollision(other.ClosestPoint(transform.position), playerData);
                _isLocked = true;
                Destroy(gameObject);
            }
        }
    }
}
