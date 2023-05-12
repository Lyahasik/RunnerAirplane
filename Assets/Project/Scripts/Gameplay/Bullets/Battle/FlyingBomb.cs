using UnityEngine;

using RunnerAirplane.Core;
using RunnerAirplane.Gameplay.Objects;
using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Bullets.Battle
{
    public class FlyingBomb : Bullet
    {
        private const float _bottomLineDestruction = 0f;
        
        private int _damage;
        [SerializeField] private float _fallingSpeed;
        [SerializeField] private GameObject _bomb;
        [SerializeField] private float _explosionScale;
        private float _startPositionY;
        
        private bool _isActive;

        private void Awake()
        {
            _startPositionY = _bomb.transform.position.y;
        }

        public override void Init(Vector3 position, int damage = 0)
        {
            transform.position = position;
            _bomb.SetActive(true);
            _bomb.transform.position =
                new Vector3(transform.position.x, _startPositionY, transform.position.z);
            
            _damage = damage;

            _isActive = true;
        }

        private void Update()
        {
            if (LevelHandler.PauseGame)
                return;
            
            Movement();
        }

        private void Movement()
        {
            if (!_isActive)
                return;
            
            _bomb.transform.position += Vector3.down * _fallingSpeed * Time.deltaTime;

            if (_bomb.transform.position.y < _bottomLineDestruction)
            {
                Explosion();
            }
        }

        private void Explosion()
        {
            Bullet bullet = _poolBullets.GetBullet(BulletType.ExplosionBullet);
            bullet.Init(transform.position, _damage);
            bullet.transform.localScale = new Vector3(_explosionScale, _explosionScale, _explosionScale);
            
            _poolBullets.ReturnBullet(this, BulletType.FlyingBomb);
            _bomb.SetActive(false);
        }

        public override void Reset(Vector3 newPosition)
        {
            _isActive = false;
            transform.position = newPosition;
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerData playerData = other.GetComponent<PlayerData>();
            
            if (playerData)
            {
                playerData.CalculateNewHealth(MathOperationType.Subtraction, _damage);
                _poolBullets.ReturnBullet(this, BulletType.FlyingBomb);
            }
        }
    }
}
