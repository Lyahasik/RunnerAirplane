using UnityEngine;

using RunnerAirplane.Core;
using RunnerAirplane.Helpers;

namespace RunnerAirplane.Gameplay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _rangeHorizontalMovement;
        private float _rangeVerticalMovement;
        
        [Space]
        [SerializeField] private float _speedTurn;
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _reverseTurnSpeed;

        [Space]
        [SerializeField] private float _speedAlignment;

        private bool _isFakeFakeCombating;

        public float RangeHorizontalMovement
        {
            set => _rangeHorizontalMovement = value;
        }

        public float RangeVerticalMovement
        {
            set => _rangeVerticalMovement = value;
        }

        public bool IsFakeCombating
        {
            get => _isFakeFakeCombating;
            set => _isFakeFakeCombating = value;
        }

        private void Update()
        {
            CenterAlignment();
            ReverseTurn();
        }

        private void CenterAlignment()
        {
            if (!_isFakeFakeCombating
                || transform.position.x == 0f)
                return;

            float distance = -transform.position.x;
            Vector3 direction = (Vector3.right * distance).normalized;
            
            Vector3 step = direction * _speedAlignment;
            if (Mathf.Abs(transform.position.x) < _speedAlignment * Time.deltaTime)
            {
                transform.position = new Vector3(0f, transform.position.y, transform.position.z);
                return;
            }
            
            TakeStepMove(step);
        }

        public void TakeStepMove(Vector3 step)
        {
            if (LevelHandler.PauseGame)
                return;
            
            step *= Time.deltaTime;
            
            TakeStepTurn(step.x);

            float newPositionX = Mathf.Clamp(transform.position.x + step.x, -_rangeHorizontalMovement, _rangeHorizontalMovement);
            float newPositionZ = Mathf.Clamp(transform.position.z + step.z, -_rangeVerticalMovement, _rangeVerticalMovement);
            
            Vector3 newPosition;
            newPosition = new Vector3(newPositionX, transform.position.y, newPositionZ);

            transform.position = newPosition;
        }

        private void TakeStepTurn(float step)
        {
            float angle = MyMath.RezusAngleTurnInRange(step * _speedTurn, 
                Vector3.up, 
                transform.up, 
                -transform.right,
                -_maxAngle, 
                _maxAngle);

            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -angle));
        }

        private void ReverseTurn()
        {
            float angle = MyMath.RezusAngle(Vector3.up, transform.up, -transform.right);
            
            if (angle == 0f)
                return;
            
            int rezus = angle < 0 ? -1 : 1;
            
            angle = Mathf.Clamp(Mathf.Abs(angle) - _reverseTurnSpeed * Time.deltaTime, 0f, 180f);
            angle *= rezus;
            
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -angle));
        }
    }
}
