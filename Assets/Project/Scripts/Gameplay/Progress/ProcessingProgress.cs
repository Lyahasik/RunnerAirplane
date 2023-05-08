using System;
using UnityEngine;

namespace RunnerAirplane.Gameplay.Progress
{
    public static class ProcessingProgress
    {
        private const string _stringKeyLastLevel = "LastLevel";
        private const string _stringKeyLastStartHealth = "LastStartHealth";
        private const string _stringKeySkin = "Skin";
        private const string _stringKeyActiveSkin = "ActiveSkin";

        private const string _stringKeyNumberMoney = "NumberMoney";

        public static event Action<int, int> OnActiveSkin;
        public static event Action OnUpdateMoney;

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

        public static void UnlockSkin(int eraNumber, int skinNumber)
        {
            string key = _stringKeySkin + eraNumber + skinNumber;
            
            PlayerPrefs.SetInt(key, 1);
        }

        public static bool CheckUnlockSkin(int eraNumber, int skinNumber)
        {
            string key = _stringKeySkin + eraNumber + skinNumber;

            bool isUnlock = PlayerPrefs.GetInt(key) == 1;

            return isUnlock;
        }

        public static void ActivateSkin(int eraNumber, int skinNumber)
        {
            string key = _stringKeyActiveSkin + eraNumber;
            
            PlayerPrefs.SetInt(key, skinNumber);
            OnActiveSkin?.Invoke(eraNumber, skinNumber);
        }

        public static bool CheckActivateSkin(int eraNumber, int skinNumber)
        {
            string key = _stringKeyActiveSkin + eraNumber;
        
            bool isActive = PlayerPrefs.GetInt(key) == skinNumber;
        
            return isActive;
        }

        public static void UpdateNumberMoney(int value)
        {
            int currentNumberMoney = PlayerPrefs.GetInt(_stringKeyNumberMoney);

            currentNumberMoney += value;
            
            PlayerPrefs.SetInt(_stringKeyNumberMoney, currentNumberMoney);
            OnUpdateMoney?.Invoke();
        }

        public static int GetNumberMoney()
        {
            return PlayerPrefs.GetInt(_stringKeyNumberMoney);
        }

        public static void ResetAll()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
