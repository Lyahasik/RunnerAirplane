using System;
using UnityEngine;

namespace RunnerAirplane.Gameplay.Objects.Obstacles
{
    public class MovementToPoint : MonoBehaviour
    {
        [SerializeField] private float _speedMove;
        [SerializeField] private Transform[] _positions;

        private float _distantionMove;
        private Vector3 _directionMove;
        private Transform _target;
        private int _targetId;

        private void Start()
        {
            NextTarget();
        }

        private void Update()
        {
            StepMove();
        }

        private void StepMove()
        {
            float distantionStep = _speedMove * Time.deltaTime;
            Vector3 step;
            
            if (_distantionMove - Math.Abs(distantionStep) < 0)
            {
                step = _directionMove * distantionStep;
                NextTarget();
            }
            else
            {
                step = _directionMove * distantionStep;
                _distantionMove -= distantionStep;
            }

            step = transform.TransformDirection(step);
            transform.Translate(step);
        }

        private void NextTarget()
        {
            _target = _positions[_targetId];
            
            Vector3 vectorMove = _target.position - transform.position;
            _distantionMove = Vector3.Magnitude(vectorMove);
            _directionMove = Vector3.Normalize(vectorMove);
            
            _targetId = (_targetId + 1) % _positions.Length;
        }
    }
}
