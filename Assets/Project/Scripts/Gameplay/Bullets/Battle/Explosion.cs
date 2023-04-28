using UnityEngine;

using RunnerAirplane.Gameplay.Objects;
using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Bullets.Battle
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;
        private int _damage;

        private bool _isHit;

        public void Init(int damage)
        {
            _damage = damage;
            Destroy(gameObject, _lifeTime);
        }

        private void MakeDamage(PlayerData playerData)
        {
            if (_isHit)
                return;
            
            playerData.CalculateNewHealth(MathOperationType.Subtraction, _damage);
            _isHit = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerData playerData = other.GetComponent<PlayerData>();
            
            if (playerData)
            {
                MakeDamage(playerData);
            }
        }
    }
}
