
import Foundation
import CoreHaptics
import AudioToolbox
import UIKit


@objc public enum EVibrationType : Int
{
    case LightImpact
    case MediumImpact
    case HeavyImpact
    case Success
    case Warning
    case Failure
};


@objc public class UnityPlugin : NSObject {
    
    @objc public static let shared = UnityPlugin()
    
    @available(iOS 13.0, *)
    @objc public func IsHapticCustomAvailable() -> Bool
    {
        return CHHapticEngine.capabilitiesForHardware().supportsHaptics;
    }
    
    @available(iOS 13.0, *)
    @objc public func PlayCustom(intens:Float,sharp:Float ,dur: Double) -> Void
    {
        var engine : CHHapticEngine!;
        
        guard CHHapticEngine.capabilitiesForHardware().supportsHaptics else
        {
            print("Device not support CHHapticEngine");
            return;
        }
        
        do
        {
            engine = try CHHapticEngine();
        }
        catch let error
        {
            dump(error);
        }
        
        engine?.start(completionHandler: { (error) in
            
         })
        
        engine?.stoppedHandler = { reason in
          print("Haptic engine stopped due to reason: \(reason)")
        }
        
        engine?.resetHandler = {
            
            print("Reset Handler: Restarting the engine.")
            
            do {
                print("Try restart haptic engine")
                try engine.start()

            } catch {
                fatalError("Failed to restart the Haptic engine: \(error)")
            }
        }
        
        guard CHHapticEngine.capabilitiesForHardware().supportsHaptics else { return }

        let intensity = CHHapticEventParameter(parameterID: .hapticIntensity, value: intens)
        let sharpness = CHHapticEventParameter(parameterID: .hapticSharpness, value: sharp)
        
        let event = CHHapticEvent(eventType: dur > 0 ? .hapticContinuous : .hapticTransient, parameters: [intensity, sharpness], relativeTime: 0, duration: dur)
        
        do
        {
           let pattern = try CHHapticPattern(events: [event], parameters: [])
           let player = try engine?.makePlayer(with: pattern)
           try player?.start(atTime: 0)
            
        }
        catch
        {
           print("Failed to play pattern: \(error.localizedDescription).")
        }
    }
    
    @objc public func PlayHaptic(typeInt : Int) -> Void
    {
        var type : EVibrationType!;
        
        type = EVibrationType(rawValue: typeInt);
        
        if #available(iOS 10.0, *) {
            PlayCoolHaptic(hType : type);
        } else {
            PlayHapticIphone6(hType : type);
        }
    }
    
    @objc public func PlayCoolHaptic(hType : EVibrationType) -> Void
    {
        
        switch(hType)
        {
            case .LightImpact:
            
            let generator = UIImpactFeedbackGenerator(style: .light)
            generator.impactOccurred()
            
            case .MediumImpact:
            
            let generator = UIImpactFeedbackGenerator(style: .medium)
            generator.impactOccurred()
            
            case .HeavyImpact:
            
            let generator = UIImpactFeedbackGenerator(style: .heavy)
            generator.impactOccurred()
            
            case .Failure:
            
            let generator = UINotificationFeedbackGenerator()
            generator.notificationOccurred(.error)
            
            case .Warning:
            
            let generator = UINotificationFeedbackGenerator()
            generator.notificationOccurred(.warning)
            
            case .Success:
            
            let generator = UINotificationFeedbackGenerator()
            generator.notificationOccurred(.success)
        
            
            default :
            print("Unknow vibration type");
        }
     
    }
    
    public func PlayHapticIphone6(hType : EVibrationType) -> Void
    {
       
        switch(hType)
        {
            case .LightImpact:
            
            AudioServicesPlaySystemSound(1519);
            
            case .MediumImpact:
            
            AudioServicesPlaySystemSound(1520);
            
            case .HeavyImpact:
            
            AudioServicesPlaySystemSound(1520);
            
            case .Failure:
            
            AudioServicesPlaySystemSound(1521);
            
            case .Warning:
            
            AudioServicesPlaySystemSound(1521);
            
            case .Success:
            
            AudioServicesPlaySystemSound(1521);
            
            default :
            print("Unknow vibration type");
        }
    }
}

