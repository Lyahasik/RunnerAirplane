using UnityEngine;

namespace RunnerAirplane.Gameplay.Bosses
{
    public class SpyingPlayer : MonoBehaviour
    {
        [SerializeField] private Transform _player;

        private bool _isTurning;

        public bool IsTurning
        {
            set => _isTurning = value;
        }

        private void Update()
        {
            LookPlayer();
        }

        private void LookPlayer()
        {
            if (_isTurning)
                transform.LookAt(_player, Vector3.up);
            else
                transform.LookAt(transform.position + Vector3.back, Vector3.up);
        }
    }
}
