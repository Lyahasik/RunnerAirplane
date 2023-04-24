using TMPro;
using UnityEngine;

using RunnerAirplane.Gameplay.Player;

namespace RunnerAirplane.Gameplay.Objects
{
    public class StatChanger : MonoBehaviour
    {
        [SerializeField] protected MathOperationType _operationType;
        [SerializeField] protected int _value;

        [Space]
        [SerializeField] private TMP_Text _text;

        protected bool _isLocked;

        private void Start()
        {
            InitText();
        }

        protected virtual void InitText()
        {
            if (!_text)
                return;
            
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

        public virtual void ProcessingPlayerData(PlayerData playerData)
        {
            if (_isLocked)
                return;
            
            playerData.CalculateNewHealth(_operationType, _value);
            _isLocked = true;
            Destroy(gameObject);
        }
        
        protected virtual void OnTriggerEnter(Collider other)
        {
            PlayerData playerData = other.GetComponent<PlayerData>();

            if (playerData)
            {
                ProcessingPlayerData(playerData);
            }
        }
    }
}
