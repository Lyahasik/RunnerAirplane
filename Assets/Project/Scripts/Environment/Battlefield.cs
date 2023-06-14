using System.Collections.Generic;
using UnityEngine;

using RunnerAirplane.Gameplay;

namespace RunnerAirplane.Environment
{
    public class Battlefield : MonoBehaviour
    {
        private const float _scaleFactorPlane = 10f;
        
        [SerializeField] private GameObject _pointTarget;
        
        [Space]
        [SerializeField] private bool _isMovingBattlefield;
        [SerializeField] private float _speedMoveRoad;
        [SerializeField] private float _speedMoveObjects;
        [SerializeField] private List<GameObject> _movedObjects;

        private MovementToPoint _movementToPoint;

        [SerializeField] private MeshRenderer _meshRoad;
        private Material _materialRoad;
        private Vector2 _offsetRoad;
        
        private float _halfLength;

        private bool _isMoved;

        public bool IsReady => _movementToPoint.IsStop;

        private void Awake()
        {
            _movementToPoint = GetComponent<MovementToPoint>();

            InitMaterials();

            _halfLength = _meshRoad.gameObject.transform.localScale.z * 0.5f * _scaleFactorPlane;
        }

        private void Update()
        {
            MovementBattle();
        }

        private void InitMaterials()
        {
            _materialRoad = _meshRoad.material;
            _offsetRoad = _materialRoad.mainTextureOffset;
        }

        public void StartMovement()
        {
            _pointTarget.transform.parent = transform.parent;
            _movementToPoint.enabled = true;

            _isMoved = true;
        }
        
        private void MovementBattle()
        {
            if (!_isMoved
                || !_isMovingBattlefield)
                return;
                
            MovementRoad();
            MovementObjects();
        }

        private void MovementRoad()
        {
            float stepRoad = _speedMoveRoad * Time.deltaTime;

            _offsetRoad.y -= stepRoad / _materialRoad.mainTextureScale.y;
            _materialRoad.mainTextureOffset = _offsetRoad;
        }

        private void MovementObjects()
        {
            float stepObjects = _speedMoveObjects * Time.deltaTime;

            float backBorder = transform.position.z - _halfLength;
            foreach (GameObject movedObject in _movedObjects)
            {
                movedObject.transform.position += Vector3.back * stepObjects;

                if (movedObject.transform.position.z <= backBorder)
                    movedObject.transform.position
                        += Vector3.forward * _meshRoad.gameObject.transform.localScale.z * _scaleFactorPlane;
            }
        }
    }
}
