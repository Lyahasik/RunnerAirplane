using TMPro;
using UnityEngine;

using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Events.Gates
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private MathOperationType _operationType;
        [SerializeField] private int _value;

        [Space]
        [SerializeField] private TMP_Text _text;

        private bool _isLocked;

        private void Start()
        {
            InitText();
        }

        private void InitText()
        {
            string text = "???";

            switch (_operationType)
            {
                case MathOperationType.Addition:
                    text = $"+{_value}";
                    break;
                case MathOperationType.Subtraction:
                    text = $"-{_value}";
                    break;
                case MathOperationType.Multiplication:
                    text = $"*{_value}";
                    break;
                case MathOperationType.Division:
                    text = $"/{_value}";
                    break;
            }

            _text.text = text;
        }

        public void ProcessingPlayerData(PlayerData playerData)
        {
            if (_isLocked)
                return;
            
            playerData.CalculateNewHealth(_operationType, _value);
            _isLocked = true;
            Destroy(gameObject);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            PlayerData playerData = other.GetComponent<PlayerData>();

            if (playerData)
            {
                ProcessingPlayerData(playerData);
            }
        }
    }
}
