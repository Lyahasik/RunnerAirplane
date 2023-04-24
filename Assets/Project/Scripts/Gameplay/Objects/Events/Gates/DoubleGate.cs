using UnityEngine;

using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Objects.Events.Gates
{
    public class DoubleGate : MonoBehaviour
    {
        [SerializeField] private Gate _leftGate;
        [SerializeField] private Gate _rightGate;

        private void ProcessingCollision(Vector3 point, PlayerData playerData)
        {
            if (point.x < 0f && _leftGate)
            {
                _leftGate.ProcessingPlayerData(playerData);
            }
            else if (_rightGate)
            {
                _rightGate.ProcessingPlayerData(playerData);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerData playerData = other.GetComponent<PlayerData>();

            if (playerData)
            {
                ProcessingCollision(other.ClosestPoint(transform.position), playerData);
            }
        }
    }
}
