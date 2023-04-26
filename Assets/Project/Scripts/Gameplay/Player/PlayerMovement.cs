using UnityEngine;

using RunnerAirplane.Helpers;

namespace RunnerAirplane.Gameplay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _rangeMovement;
        
        [Space]
        [SerializeField] private float _speedTurn;
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _reverseTurnSpeed;

        [Space]
        [SerializeField] private float _speedAlignment;

        private bool _isCombating;

        public bool IsCombating
        {
            get => _isCombating;
            set => _isCombating = value;
        }

        private void Update()
        {
            CenterAlignment();
            ReverseTurn();
        }

        private void CenterAlignment()
        {
            if (!_isCombating
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
            step *= Time.deltaTime;
            
            TakeStepTurn(step.x);

            float newPositionX = Mathf.Clamp(transform.position.x + step.x, -_rangeMovement, _rangeMovement);
            Vector3 newPosition = new Vector3(newPositionX, transform.position.y, transform.position.z);
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
