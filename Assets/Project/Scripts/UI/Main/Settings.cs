using UnityEngine;
using UnityEngine.UI;

using RunnerAirplane.Core;
using RunnerAirplane.Core.Audio;

namespace RunnerAirplane.UI.Main
{
    public class Settings : MonoBehaviour
    {
        private const string _keyMusic = "Music";
        private const string _keySounds = "Sounds";
        private const string _keyVibration = "Vibration";
        
        [SerializeField] private AudioHandler _audioHandler;

        [Space]
        [SerializeField] private Toggle _toggleMusic;
        [SerializeField] private Toggle _toggleSounds;
        [SerializeField] private Toggle _toggleVibration;

        public static bool VibrationOn;

        private bool _isStartedGame;

        public void UpdateSettings()
        {
            int music = PlayerPrefs.GetInt(_keyMusic);
            int sounds = PlayerPrefs.GetInt(_keySounds);
            int vibration = PlayerPrefs.GetInt(_keyVibration);
            
            if (music == 0)
                SwitchMusic(true);
            else
                SwitchMusic(music == 1);
            
            if (sounds == 0)
                SwitchSounds(true);
            else
                SwitchSounds(sounds == 1);
            
            if (vibration == 0)
                SwitchVibration(true);
            else
                SwitchVibration(vibration == 1);
        }

        private void OnEnable()
        {
            SwitchMusic(PlayerPrefs.GetInt(_keyMusic) == 1);
            SwitchVibration(PlayerPrefs.GetInt(_keyVibration) == 1);
            
            _audioHandler.SetSoundsMute(true);

            if (!LevelHandler.PauseGame)
                _isStartedGame = true;
            
            LevelHandler.PauseGame = true;
        }

        private void OnDisable()
        {
            SwitchSounds(PlayerPrefs.GetInt(_keySounds) == 1);
            
            if (_isStartedGame)
                LevelHandler.PauseGame = false;
        }

        public void SwitchMusic(bool value)
        {
            _toggleMusic.isOn = value;
            _audioHandler.SetMusicMute(!value);
            
            PlayerPrefs.SetInt(_keyMusic, value ? 1 : -1);
        }

        public void SwitchSounds(bool value)
        {
            _toggleSounds.isOn = value;
            _audioHandler.SetSoundsMute(!value);
            
            PlayerPrefs.SetInt(_keySounds, value ? 1 : -1);
        }

        public void SwitchVibration(bool value)
        {
            _toggleVibration.isOn = value;
            VibrationOn = value;
            
            PlayerPrefs.SetInt(_keyVibration, VibrationOn ? 1 : -1);
        }
    }
}
