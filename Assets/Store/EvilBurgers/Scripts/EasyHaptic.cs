using UnityEngine;

namespace EasyHaptic_EvilBurgers
{
    public static class EasyHaptic
    {
        public static VibrationEngine vibrationEngine = new VibrationEngine();

        public static bool hapticOn
        {
            get => PlayerPrefs.GetInt("EasyHapticTurnOn", 1) > 0 ? true : false;
            set => PlayerPrefs.SetInt("EasyHapticTurnOn", value == true ? 1 : 0);
        }

        /// <summary>
        /// Play vibration by EVibrationType
        /// </summary>
        /// <param name="vibrationType"></param>
        public static void Play(EVibrationType vibrationType)
        {
#if UNITY_EDITOR
            return;
#endif
            if(hapticOn)
                vibrationEngine.PlayTyped(vibrationType);
        }

        /// <summary>
        /// Play custom vibration, you need create new CustomVibrationData obj;
        /// </summary>
        /// <param name="customVibration"></param>
        public static void PlayCustom(CustomVibrationData customVibration)
        {
#if UNITY_EDITOR
            return;
#endif
            if (hapticOn)
                vibrationEngine.PlayCustomVibro(customVibration);
        }
    }
}

// Predifined types, do not modify this. If wanna custom vibro use CustomVibrationData 
public enum EVibrationType
{
    LightImpact,
    MediumImpact,
    HeavyImpact,
    Success,
    Warning,
    Failure
}

// Data for playing custom Vibrations
[System.Serializable]
public class CustomVibrationData
{
    public double durationInSeconds;

    /// <summary>
    /// <value> Amplitude must between 0-100, Remember some Android devices dont have amplitude controller you can play only with Duration value </value>
    /// </summary>
    [Range(0,100)] public float amplitude;

    /// <summary>
    /// <value> Sharpness only for IOS </value>-
    /// </summary>
    [Header("Only for IOS")][Range(0, 100)] public float sharpness;
}

