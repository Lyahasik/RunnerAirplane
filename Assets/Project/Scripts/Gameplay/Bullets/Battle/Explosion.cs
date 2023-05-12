using UnityEngine;

using RunnerAirplane.Gameplay.Objects;
using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Bullets.Battle
{
    public class Explosion : Bullet
    {
        [SerializeField] private BulletType _type;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private float _lifeTime;
        private int _damage;

        private bool _isHit;

        public override void Init(Vector3 position, int damage = 0)
        {
            transform.position = position;
            
            _damage = damage;
            _particleSystem.Play();
            Invoke(nameof(ReturnBullet), _lifeTime);
        }

        private void ReturnBullet()
        {
            _particleSystem.Stop();
            _poolBullets.ReturnBullet(this, _type);
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

        public override void Reset(Vector3 newPosition)
        {
            transform.position = newPosition;
        }
    }
}
