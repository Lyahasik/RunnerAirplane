using UnityEngine;

namespace RunnerAirplane
{
    public class RestoreMonetizationButton : MonoBehaviour
    {
        private void Awake()
        {
            #if UNITY_ANDROID
                Destroy(gameObject);
            #endif
        }
    }
}
