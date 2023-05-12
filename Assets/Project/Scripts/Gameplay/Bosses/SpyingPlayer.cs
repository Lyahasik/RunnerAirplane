using UnityEngine;

namespace RunnerAirplane.Gameplay.Bosses
{
    public class SpyingPlayer : MonoBehaviour
    {
        [SerializeField] private Transform _player;

        private void Update()
        {
            transform.LookAt(_player, Vector3.up);
        }
    }
}
