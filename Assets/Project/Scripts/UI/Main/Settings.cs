using System;
using UnityEngine;

namespace RunnerAirplane
{
    public class Settings : MonoBehaviour
    {
        private const string _keyAudio = "Audio";
        private const string _keyVibration = "Vibration";

        public static bool VibrationOn;

        public void UpdateSettings()
        {
            if (PlayerPrefs.GetInt(_keyAudio) == 0)
                SwitchAudio(true);
            
            if (PlayerPrefs.GetInt(_keyVibration) == 0)
                SwitchVibration(true);
        }

        private void OnEnable()
        {
            SwitchAudio(PlayerPrefs.GetInt(_keyAudio) == 1);
            SwitchVibration(PlayerPrefs.GetInt(_keyVibration) == 1);
        }

        public void SwitchAudio(bool value)
        {
            AudioListener.volume = value ? 1f : 0f;
            
            PlayerPrefs.SetInt(_keyAudio, value ? 1 : -1);
        }

        public void SwitchVibration(bool value)
        {
            VibrationOn = value;
            
            PlayerPrefs.SetInt(_keyVibration, VibrationOn ? 1 : -1);
        }
    }
}
