using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

using RunnerAirplane.Gameplay.Bosses;
using RunnerAirplane.Gameplay.Objects.Events.Gates;
using RunnerAirplane.Gameplay.Player;
using RunnerAirplane.Gameplay.Progress;
using RunnerAirplane.UI.Level;

namespace RunnerAirplane.Core
{
    public class LevelHandler : MonoBehaviour
    {
        private const int _levelMultiplier = 10;
        
        [SerializeField] private LevelMenu _levelMenu;
        
        [Space]
        [SerializeField] private float _speedMove;
        [SerializeField] private GameObject _road;
        [SerializeField] private bool _isMovingBattlefield;
        [SerializeField] private float _speedMoveBattlefield;
        [SerializeField] private GameObject _battlefield;
        [SerializeField] private GameObject _movingObjects;

        [Space]
        [SerializeField] private float _startBattlePositionZ;
        [SerializeField] private Vector3 _battleCameraPosition;
        [SerializeField] private Vector3 _battleCameraRotation;
        [SerializeField] private float _speedChange;

        [Space] [SerializeField] private GameObject _player;
        [SerializeField] private float _battleRangeHorizontalMovement;
        [SerializeField] private float _battleRangeVerticalMovement;
        [SerializeField] private List<Boss> _bosses;
        private PlayerData _playerData;
        private PlayerMovement _playerMovement;
        private PlayerAttack _playerAttack;

        private Material _materialRoad;
        private Material _materialBattlefield;
        private Vector2 _offset;
        private Vector2 _offsetBattle;

        private Camera _camera;
        private Vector3 _startCameraPosition;
        private float _distanceBetweenOffsets;
        private Quaternion _startCameraRotation;
        private Quaternion _endCameraRotation;
        
        private bool _isBattle;
        private bool _isActiveBattle;

        private bool _endGame;

        private void Start()
        {
            _camera = Camera.main;
            _startCameraPosition = _camera.transform.position;
            _startCameraRotation = _camera.transform.rotation;
            
            _playerData = _player.GetComponent<PlayerData>();
            _playerMovement = _player.GetComponent<PlayerMovement>();
            _playerAttack = _player.GetComponent<PlayerAttack>();
            
            InitMaterials();
        }

        private void OnEnable()
        {
            FinishGate.OnFinish += SuccessLevel;
        }

        private void OnDisable()
        {
            FinishGate.OnFinish -= SuccessLevel;
        }

        private void InitMaterials()
        {
            _materialRoad = _road.GetComponent<MeshRenderer>().material;
            _offset = _materialRoad.mainTextureOffset;

            if (_battlefield)
            {
                _materialBattlefield = _battlefield.GetComponent<MeshRenderer>().material;
                _offsetBattle = _materialBattlefield.mainTextureOffset;
            }
        }

        private void Update()
        {
            if (_endGame)
                return;
            
            Movement();
            MovementBattle();
            PrepareBattle();
            TryFinishLevel();
        }

        private void Movement()
        {
            if (_isBattle)
                return;
            
            float step = _speedMove * Time.deltaTime;

            _offset.y -= step / _materialRoad.mainTextureScale.y;
            _materialRoad.mainTextureOffset = _offset;
            
            _movingObjects.transform.Translate(new Vector3(0f, 0f, -step));

            if (_battlefield
                && _battlefield.transform.position.z <= _startBattlePositionZ)
                ActivatePrepareBattle();
        }

        private void ActivatePrepareBattle()
        {
            _isBattle = true;
            
            _camera.GetComponent<CinemachineBrain>().enabled = false;
            _startCameraPosition = _camera.transform.position;
            _distanceBetweenOffsets = Vector3.Distance(_startCameraPosition, _battleCameraPosition);
            
            _endCameraRotation = Quaternion.Euler(_battleCameraRotation);
        }

        private void PrepareBattle()
        {
            if (!_isBattle
                || _isActiveBattle
                || _camera.transform.position == _battleCameraPosition)
                return;

            float currentDistance = Vector3.Distance(_startCameraPosition, _camera.transform.position);
            float t = currentDistance / _distanceBetweenOffsets + _speedChange * Time.deltaTime;
        
            _camera.transform.position = Vector3.Lerp(_startCameraPosition, _battleCameraPosition, t);
            _camera.transform.rotation = Quaternion.Lerp(_startCameraRotation, _endCameraRotation, t);

            if (_camera.transform.position == _battleCameraPosition)
                StartBattle();
        }

        private void MovementBattle()
        {
            if (!_isBattle
                || !_isMovingBattlefield)
                return;
                
            float step = _speedMoveBattlefield * Time.deltaTime;

            _offsetBattle.y -= step / _materialBattlefield.mainTextureScale.y;
            _materialBattlefield.mainTextureOffset = _offsetBattle;
        }

        private void StartBattle()
        {
            _playerMovement.RangeHorizontalMovement = _battleRangeHorizontalMovement;
            _playerMovement.RangeVerticalMovement = _battleRangeVerticalMovement;
            
            _isActiveBattle = true;

            foreach (Boss boss in _bosses)
            {
                boss.enabled = true;
            }
            
            _playerAttack.IsActive = true;
        }

        private void TryFinishLevel()
        {
            if (_isActiveBattle)
            {
                bool bossAlive = _bosses.Any(boss => boss);

                if (!bossAlive)
                {
                    SuccessLevel();
                
                    _playerAttack.IsActive = false;
                }

                if (!_player)
                {
                    GameOver();

                    foreach (Boss boss in _bosses)
                    {
                        if (boss)
                        {
                            boss.EndBattle();
                            boss.enabled = false;
                        }
                    }
                }
            }
            else if (!_player)
            {
                GameOver();
            }
        }

        private void SuccessLevel()
        {
            _endGame = true;

            int money = _playerData.CurrentHealth +
                        _playerData.CurrentHealth * (SceneManager.GetActiveScene().buildIndex / _levelMultiplier);
            ProcessingProgress.UpdateNumberMoney(money);
            _levelMenu.SuccessGame(money);

            ProcessingProgress.RememberLastStartHealth(_playerData.StartHealth);
            ProcessingProgress.RememberLastLevel(SceneManager.GetActiveScene().buildIndex);
        }

        private void GameOver()
        {
            _endGame = true;
            _levelMenu.GameOver();
        }
    }
}
