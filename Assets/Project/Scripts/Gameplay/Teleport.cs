using System.Collections.Generic;
using UnityEngine;

namespace RunnerAirplane.Gameplay
{
    [RequireComponent(typeof(MovementToPoint))]
    public class Teleport : MonoBehaviour
    {
        private MovementToPoint _movementToPoint;
        
        [SerializeField] private List<GameObject> _teleportPoints;
        [SerializeField] private float _delayTeleport;
        private float _nextTimeTeleport;

        private void Awake()
        {
            _movementToPoint = GetComponent<MovementToPoint>();
            _nextTimeTeleport = Time.time + _delayTeleport;
        }

        private void Update()
        {
            NextTeleport();
        }

        private void NextTeleport()
        {
            if (_nextTimeTeleport > Time.time)
                return;

            int index = Random.Range(0, _teleportPoints.Count);
            transform.position = _teleportPoints[index].transform.position;
            _movementToPoint.NextTarget();
            
            _nextTimeTeleport = Time.time + _delayTeleport;
        }
    }
}
