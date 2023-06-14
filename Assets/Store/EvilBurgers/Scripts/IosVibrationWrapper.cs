using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public sealed class IosVibrationWrapper 
{

#if UNITY_IOS

    [DllImport("__Internal")]
    private static extern void _PlayCustom(float intensity, float sharpness, double duration);

    [DllImport("__Internal")]
    private static extern void _PlayHaptic(int type);

#endif
   

    public void PlayCustom(CustomVibrationData customVibration)
    {
#if UNITY_IOS
        _PlayCustom(customVibration.amplitude, customVibration.sharpness, customVibration.durationInSeconds);
#endif
    }

    public void PlayHaptic(EVibrationType type)
    {
#if UNITY_IOS
        _PlayHaptic((int)type);
#endif
    }
}
