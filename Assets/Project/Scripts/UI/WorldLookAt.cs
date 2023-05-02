using UnityEngine;

namespace RunnerAirplane.UI
{
    public class WorldLookAt : MonoBehaviour
    {
        private Camera _camera;

        [SerializeField] private bool _isBattle;

        public bool IsBattle
        {
            set => _isBattle = value;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            LookCamera();
        }

        private void LookCamera()
        {
            if (_isBattle)
            {
                Quaternion rotation = Quaternion.LookRotation(Vector3.down);
                transform.rotation = rotation;
            }
            else
            {
                Vector3 direction = transform.position - _camera.transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = rotation;
            }
        }
    }
}
