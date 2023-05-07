using UnityEngine;

namespace RunnerAirplane.Gameplay.Progress
{
    public static class ProcessingProgress
    {
        private const string _stringKeyLastLevel = "LastLevel";
        private const string _stringKeyLastStartHealth = "LastStartHealth";
        private const string _stringKeySkin = "Skin";

        public static void RememberLastLevel(int number)
        {
            PlayerPrefs.SetInt(_stringKeyLastLevel, number);
        }
        
        public static int GetLastLevel()
        {
            return PlayerPrefs.GetInt(_stringKeyLastLevel);
        }
        
        public static void RememberLastStartHealth(int health)
        {
            PlayerPrefs.SetInt(_stringKeyLastStartHealth, health);
        }
        
        public static int GetLastStartHealth()
        {
            return PlayerPrefs.GetInt(_stringKeyLastStartHealth);
        }

        public static void ActivateSkin(int eraNumber, int skinNumber)
        {
            string key = _stringKeySkin + eraNumber + skinNumber;
            
            Debug.Log($"Save {key}");
            PlayerPrefs.SetInt(key, 1);
        }

        public static bool CheckSkin(int eraNumber, int skinNumber)
        {
            string key = _stringKeySkin + eraNumber + skinNumber;

            Debug.Log($"Check {key}");
            bool isActive = PlayerPrefs.GetInt(key) == 1;

            return isActive;
        }

        public static void ResetAll()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
