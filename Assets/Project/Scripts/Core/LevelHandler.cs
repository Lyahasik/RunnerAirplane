using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

using RunnerAirplane.Core.Audio;
using RunnerAirplane.Environment;
using RunnerAirplane.Gameplay;
using RunnerAirplane.Gameplay.Bosses;
using RunnerAirplane.Gameplay.Objects.Events.Gates;
using RunnerAirplane.Gameplay.Player;
using RunnerAirplane.Gameplay.Progress;
using RunnerAirplane.UI.Level;

using AudioType = RunnerAirplane.Core.Audio.AudioType;

namespace RunnerAirplane.Core
{
    public class LevelHandler : MonoBehaviour
    {
        private const int _levelMultiplier = 10;
        
        private AudioHandler _audioHandler;
        
        [SerializeField] private LevelMenu _levelMenu;
        [SerializeField] private GameObject _menuWindow;
        
        [Space]
        [SerializeField] private float _speedMove;
        [SerializeField] private GameObject _clouds;
        [SerializeField] private float _offsetDown;
        [SerializeField] private Battlefield _battlefield;
        [SerializeField] private GameObject _movingObjects;

        [Space]
        [SerializeField] private bool _isReadyBattlefield;
        [SerializeField] private float _startBattlePositionZ;
        [SerializeField] private Vector3 _battleCameraPosition;
        [SerializeField] private Vector3 _battleCameraRotation;
        [SerializeField] private float _speedChange;

        [Space]
        [SerializeField] private GameObject _player;
        [SerializeField] private float _battleRangeHorizontalMovement;
        [SerializeField] private float _battleRangeVerticalMovement;
        [SerializeField] private List<Boss> _bosses;
        private PlayerData _playerData;
        private PlayerMovement _playerMovement;
        private PlayerAttack _playerAttack;

        private Camera _camera;
        private Vector3 _startCameraPosition;
        private float _distanceBetweenOffsets;
        private Quaternion _startCameraRotation;
        private Quaternion _endCameraRotation;

        private bool _isActiveGame;
        private bool _isBattle;
        private bool _isActiveBattle;

        public static bool PauseGame = true;

        public static event Action OnStartBattle;

        private void Start()
        {
            _audioHandler = FindObjectOfType<AudioHandler>();
            _audioHandler.PlayMusic(AudioType.MusicMenu);
                
            _camera = Camera.main;
            _startCameraPosition = _camera.transform.position;
            _startCameraRotation = _camera.transform.rotation;
            
            _playerData = _player.GetComponent<PlayerData>();
            _playerMovement = _player.GetComponent<PlayerMovement>();
            _playerAttack = _player.GetComponent<PlayerAttack>();

            _clouds.transform.Translate(Vector3.down * _offsetDown);
        }

        private void OnEnable()
        {
            FinishGate.OnFinish += SuccessLevel;
        }

        private void OnDisable()
        {
            FinishGate.OnFinish -= SuccessLevel;
            PauseGame = true;
        }

        private void Update()
        {
            if (PauseGame)
                return;

            if (!PauseGame && !_isActiveGame)
            {
                _isActiveGame = true;
                
                _audioHandler.StopMusic(AudioType.MusicMenu);
                _audioHandler.PlayMusic(AudioType.MusicGame);
                _playerData.ActiveAudio(true);
                
                if (_menuWindow)
                    _menuWindow.SetActive(false);
            }

            Movement();
            PrepareBattle();
            TryFinishLevel();
        }

        private void Movement()
        {
            if (_isBattle)
                return;
            
            float step = _speedMove * Time.deltaTime;
            
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

            _clouds.GetComponent<MovementToPoint>().enabled = true;
            
            if (!_isReadyBattlefield)
            {
                _battlefield.StartMovement();
            }
            
            _endCameraRotation = Quaternion.Euler(_battleCameraRotation);
        }

        private void PrepareBattle()
        {
            if (!_isBattle
                || (!_isReadyBattlefield && !_battlefield.IsReady)
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

        private void StartBattle()
        {
            _playerMovement.RangeHorizontalMovement = _battleRangeHorizontalMovement;
            _playerMovement.RangeVerticalMovement = _battleRangeVerticalMovement;
            
            _isActiveBattle = true;
            OnStartBattle?.Invoke();

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
            PauseGame = true;
            _isActiveBattle = false;

            int money = _playerData.CurrentHealth +
                        _playerData.CurrentHealth * (SceneManager.GetActiveScene().buildIndex / _levelMultiplier);
            ProcessingProgress.UpdateNumberMoney(money);
            _levelMenu.SuccessGame(money);

            ProcessingProgress.RememberLastStartHealth(_playerData.StartHealth);
            ProcessingProgress.RememberLastLevel(SceneManager.GetActiveScene().buildIndex);
        }

        private void GameOver()
        {
            PauseGame = true;
            _isActiveBattle = false;
            
            _levelMenu.GameOver();
        }
    }
}
