using UnityEngine;

using RunnerAirplane.Helpers;

namespace RunnerAirplane.Gameplay.Bosses
{
    public class TurnRange : MonoBehaviour
    {
        [SerializeField] private float _speedTurn;
        [SerializeField] private float _rangeAngle;
        private float _targetAngle;

        private bool _isTurning;
        [SerializeField] private bool _isCircular;

        public bool IsTurning
        {
            set
            {
                _isTurning = value;
                if (_isTurning)
                {
                    _targetAngle = _rangeAngle;
                }
                else
                {
                    _speedTurn = Mathf.Abs(_speedTurn);
                }
            }
        }

        private void Update()
        {
            Turn();
            Reverse();
        }
        
        private void Turn()
        {
            if (!_isTurning)
                return;

            if (_isCircular)
            {
                transform.Rotate(Vector3.up * _speedTurn * Time.deltaTime);
                return;
            }
            
            float angle = MyMath.RezusAngleTurnInRange(_speedTurn * Time.deltaTime, 
                Vector3.forward, 
                transform.forward, 
                transform.right,
                -_rangeAngle,
                _rangeAngle);
        
            SwitchTurnDirection(angle);
            if (angle == _targetAngle)
                return;

            transform.rotation = Quaternion.Euler(0f, -angle, 0f);
        }

        private void Reverse()
        {
            if (_isTurning)
                return;
            
            float angle = MyMath.RezusAngle(Vector3.forward, transform.forward, -transform.right);
        
            if (angle == 0f)
                return;
        
            int rezus = angle < 0 ? -1 : 1;
        
            angle = Mathf.Clamp(Mathf.Abs(angle) - _speedTurn * Time.deltaTime, 0f, 180f);
            angle *= rezus;
        
            transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
        }
        
        private void SwitchTurnDirection(float angle)
        {
            if (Mathf.Abs(angle) >= Mathf.Abs(_targetAngle))
            {
                _speedTurn = -_speedTurn;
                _targetAngle = -_targetAngle;
            }
        }
    }
}
