using UnityEngine;
using UnityEngine.EventSystems;

using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.UI.Level
{
    public class MobileMovementController : MonoBehaviour, IDragHandler
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private float _touchSensitivity;

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 step = new Vector3(eventData.delta.x * _touchSensitivity, 0f, eventData.delta.y * _touchSensitivity);

            if (_playerMovement
                && !_playerMovement.IsFakeCombating)
            {
                _playerMovement.TakeStepMove(step);
            }
        }
    }
}
