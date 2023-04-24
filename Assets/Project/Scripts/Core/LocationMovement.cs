using UnityEngine;

namespace RunnerAirplane.Core
{
    public class LocationMovement : MonoBehaviour
    {
        [SerializeField] private float _speedMove;
        
        [Space]
        [SerializeField] private GameObject _road;
        [SerializeField] private GameObject _movingObjects;

        private Material _materialRoad;
        private Vector2 _offset;

        private void Start()
        {
            _materialRoad = _road.GetComponent<MeshRenderer>().material;
            _offset = _materialRoad.mainTextureOffset;
        }

        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            float step = _speedMove * Time.deltaTime;

            _offset.y -= step / _materialRoad.mainTextureScale.y;
            _materialRoad.mainTextureOffset = _offset;
            _movingObjects.transform.Translate(new Vector3(0f, 0f, -step));
        }
    }
}
