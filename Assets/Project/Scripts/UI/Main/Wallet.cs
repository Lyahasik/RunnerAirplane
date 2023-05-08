using TMPro;
using UnityEngine;

using RunnerAirplane.Gameplay.Progress;

namespace RunnerAirplane.UI.Main
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textValue;

        private void OnEnable()
        {
            UpdateMoney();
            ProcessingProgress.OnUpdateMoney += UpdateMoney;
        }

        private void OnDisable()
        {
            ProcessingProgress.OnUpdateMoney -= UpdateMoney;
        }

        private void UpdateMoney()
        {
            SetMoney(ProcessingProgress.GetNumberMoney());
        }

        public void SetMoney(int value)
        {
            _textValue.text = value.ToString();
        }
    }
}
