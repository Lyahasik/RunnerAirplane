using UnityEngine;

using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Items
{
    public class DisposableRocket : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            PlayerData playerData = other.GetComponent<PlayerData>();

            if (playerData)
            {
                playerData.IsPresenceDisposableRocket = true;
                Destroy(gameObject);
            }
        }
    }
}
