using UnityEngine;

namespace RunnerAirplane.Core
{
    public class FiringZone : MonoBehaviour
    {
        private float _leftBorder;
        private float _rightBorder;
        private float _forwardBorder;
        private float _backBorder;

        private void Awake()
        {
            InitBorders();
        }
        
    #if UNITY_EDITOR
        private void Update()
        {
            DrawBorders();
        }
        
        private void DrawBorders()
        {
            Vector3 leftForwardPoint = new Vector3(_leftBorder, 0f, _forwardBorder);
            Vector3 leftBackPoint = new Vector3(_leftBorder, 0f, _backBorder);
            Vector3 rightBackPoint = new Vector3(_rightBorder, 0f, _backBorder);
            Vector3 rightForwardPoint = new Vector3(_rightBorder, 0f, _forwardBorder);
            
            Debug.DrawLine(leftForwardPoint, leftBackPoint, Color.red);
            Debug.DrawLine(leftBackPoint, rightBackPoint, Color.red);
            Debug.DrawLine(rightBackPoint, rightForwardPoint, Color.red);
            Debug.DrawLine(rightForwardPoint, leftForwardPoint, Color.red);
        }
    #endif
        
        private void InitBorders()
        {
            float halfWidth = transform.localScale.x * 0.5f;
            float halfLength = transform.localScale.z * 0.5f;

            _leftBorder = transform.position.x - halfWidth;
            _rightBorder = transform.position.x + halfWidth;
            _backBorder = transform.position.z - halfLength;
            _forwardBorder = transform.position.z + halfLength;
        }

        public Vector3 GetRandomPointInArea()
        {
            float positionX = Random.Range(_leftBorder, _rightBorder);
            float positionZ = Random.Range(_backBorder, _forwardBorder);

            return new Vector3(positionX, transform.position.y, positionZ);
        }

        public bool IsInsideArea(Vector3 point)
        {
            return point.x >= _leftBorder
                   && point.x <= _rightBorder
                   && point.z >= _backBorder
                   && point.z <= _forwardBorder;
        }
    }
}
