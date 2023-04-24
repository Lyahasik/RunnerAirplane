using RunnerAirplane.Gameplay.Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RunnerAirplane.Core
{
    public class MobileMovementController : MonoBehaviour, IDragHandler
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private float _touchSensitivity;

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 step = new Vector3(eventData.delta.x * _touchSensitivity, 0f, 0f);
            
            if (_playerMovement)
                _playerMovement.TakeStepMove(step);
        }
    }
}
