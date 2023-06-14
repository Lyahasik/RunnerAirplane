using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RunnerAirplane.Gameplay
{
    [RequireComponent(typeof(MovementToPoint))]
    public class Teleport : MonoBehaviour
    {
        private int _idStartTeleport = Animator.StringToHash("StartTeleport");
        private int _idEndTeleport = Animator.StringToHash("EndTeleport");
        
        private MovementToPoint _movementToPoint;

        [SerializeField] private List<GameObject> _listDeactivate;
        [SerializeField] private Animator _meshAnimator;
        [SerializeField] private ParticleSystem _teleportEffect;
        [SerializeField] private List<GameObject> _teleportPoints;
        [SerializeField] private float _delayStartTeleport;
        [SerializeField] private float _delayTeleport;
        private float _nextTimeTeleport;

        private void Awake()
        {
            _movementToPoint = GetComponent<MovementToPoint>();
        }

        private void Start()
        {
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

            foreach (GameObject obj in _listDeactivate)
            {
                obj.SetActive(false);
            }
            _meshAnimator.SetTrigger(_idStartTeleport);
            _teleportEffect.Play();
            _nextTimeTeleport = Time.time + _delayTeleport;
            
            Invoke(nameof(StartTeleport), _delayStartTeleport);
        }

        private void StartTeleport()
        {
            int index = Random.Range(0, _teleportPoints.Count);
            transform.position = _teleportPoints[index].transform.position;
            
            foreach (GameObject obj in _listDeactivate)
            {
                obj.SetActive(true);
            }
            _meshAnimator.SetTrigger(_idEndTeleport);
            _teleportEffect.Play();
            _movementToPoint.NextTarget();
        }
    }
}
