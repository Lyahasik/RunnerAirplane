using UnityEngine;

namespace RunnerAirplane.Gameplay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _rangeMovement;
        
        [Space]
        [SerializeField] private float _speedTurn;
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _reverseTurnSpeed;

        private void Update()
        {
            ReverseTurn();
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
