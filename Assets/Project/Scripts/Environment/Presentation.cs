using System.Linq;
using UnityEngine;

using RunnerAirplane.Gameplay;
using RunnerAirplane.Gameplay.Progress;
using RunnerAirplane.ScriptableObjects;

namespace RunnerAirplane.Environment
{
    public class Presentation : MonoBehaviour
    {
        [SerializeField] private ListSelectedEraData _listSelectedEra;
        [SerializeField] private float _angleRotationX;
        [SerializeField] private GameObject _podium;
        [SerializeField] private  float _speedRotation = 0.2f;

        private Vector3 _startPosition;
        private Quaternion _startRotation;

        private Technique _currentSkin;

        private float _dotRotation;

        private bool _pointPressed;
        private Vector3 _pointPosition;

        private void Awake()
        {
            _startPosition = transform.position;
            _startRotation = transform.rotation;
        }

        private void OnEnable()
        {
            transform.position = _startPosition;
            transform.rotation = _startRotation;
            
            if (_currentSkin)
                Destroy(_currentSkin);

            int health = ProcessingProgress.GetLastStartHealth();
            Technique prefab = _listSelectedEra.ListEra
                .First(data => health >= data.MinValue && health <= data.MaxValue)
                .Prefab;
            
            _currentSkin = Instantiate(prefab, transform.position, Quaternion.Euler(0f, _angleRotationX, 0f));
            _currentSkin.transform.parent = transform;
        }

        private void Update()
        {
            ProcessRotation();
        }

        private void ProcessRotation()
        {
            if (_pointPressed)
            {
                float angleY = (_pointPosition.x - Input.mousePosition.x) * _speedRotation * Time.deltaTime;

                transform.RotateAround(_podium.transform.position, Vector3.up, angleY);
            }
        }

        private void OnMouseDown()
        {
            _pointPressed = true;
            _pointPosition = Input.mousePosition;
        }

        private void OnMouseUp()
        {
            _pointPressed = false;
        }
    }
}
