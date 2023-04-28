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
