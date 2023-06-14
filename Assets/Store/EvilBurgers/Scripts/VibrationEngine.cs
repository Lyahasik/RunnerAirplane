using System.Runtime.InteropServices;
using UnityEngine;
using System;
#if UNITY_IOS
using UnityEngine.iOS;
#endif
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace EasyHaptic_EvilBurgers
{
    public sealed class VibrationEngine
    {
        AndroidVibrationWrapper androidVibrationWrapper = new AndroidVibrationWrapper();
        IosVibrationWrapper iosVibrationWrapper = new IosVibrationWrapper();

        [RuntimeInitializeOnLoadMethod]
        public void GlobalInitialize()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidVibrationWrapper.Initialize();         
#endif
        }

        public void PlayCustomVibro(CustomVibrationData customVibration)
        {
#if UNITY_ANDROID && !UNITY_EDITOR

            int androidLerpAmplitude = (int)Mathf.Lerp(0, 255, (float)customVibration.amplitude / 100f);
            customVibration.amplitude = androidLerpAmplitude;

            long duration = Convert.ToInt64(customVibration.durationInSeconds * 1000);

            androidVibrationWrapper.AndroidOneShotVibration(duration, androidLerpAmplitude);

#elif UNITY_IOS && !UNITY_EDITOR
            float lerpedAmplitude = Mathf.Lerp(0, 1, (float)customVibration.amplitude / 100f);
            customVibration.amplitude = lerpedAmplitude;

            float lerpedSharpness = Mathf.Lerp(0, 1, (float)customVibration.sharpness / 100f);
            customVibration.sharpness = lerpedSharpness;

            iosVibrationWrapper.PlayCustom(customVibration);
#endif

        }

        public void PlayTyped(EVibrationType type)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidVibrationWrapper.PlayHaptic(type);
            
#elif UNITY_IOS && !UNITY_EDITOR

            iosVibrationWrapper.PlayHaptic(type);
#endif
        }

        public bool SupportAmplitudeController()
        {
#if UNITY_ANDROID
            return androidVibrationWrapper.HasAmplitudeControl();
#endif
            return false;
        }

        
    }
}

