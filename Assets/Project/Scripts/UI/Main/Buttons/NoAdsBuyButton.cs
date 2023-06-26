using UnityEngine;
using UnityEngine.Purchasing;

namespace RunnerAirplane
{
    public class NoAdsBuyButton : MonoBehaviour
    {
        private CodelessIAPButton _button;

        private void Awake()
        {
            _button = GetComponent<CodelessIAPButton>();
        }

        private void OnEnable()
        {
            // IapHandler.Single.AddListenerBuy(_button);
        }
        
        private void OnDisable()
        {
            // IapHandler.Single.RemoveListenerBuy(_button);
        }
    }
}
