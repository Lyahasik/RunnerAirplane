
#import <Foundation/Foundation.h>
#include "UnityFramework/UnityFramework-Swift.h"

extern "C" {
    
#pragma mark - Functions

    
    void _PlayHaptic(int type)
    {
        [[UnityPlugin shared] PlayHapticWithTypeInt:(type)];
    }

    void _PlayCustom(float intensity, float sharpness, double duration = 0)
    {
        if (@available(iOS 13, *))
        {
            [[UnityPlugin shared] PlayCustomWithIntens:(intensity) sharp:(sharpness) dur:(duration)];
        }
    }

    bool _IsCustomHapticAvailable()
    {
        if (@available(iOS 13, *))
        {
            return  [[UnityPlugin shared] IsHapticCustomAvailable];
        }
        else
        {
            return false;
        }
        
    }
}
