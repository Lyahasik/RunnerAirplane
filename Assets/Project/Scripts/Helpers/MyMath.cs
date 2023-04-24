using UnityEngine;

namespace RunnerAirplane
{
    public static class MyMath
    {
        public static float RezusAngleTurnInRange(float stepTurn,
            Vector3 worldUp,
            Vector3 transformUp,
            Vector3 transformForward,
            float minAngle,
            float maxAngle)
        {
            float angle = RezusAngle(worldUp, transformUp, transformForward);
        
            return Mathf.Clamp(angle + stepTurn, minAngle, maxAngle);
        }

        public static float RezusAngle(Vector3 worldUp,
            Vector3 transformUp,
            Vector3 transformForward)
        {
            float rezus = Vector3.Dot(worldUp, transformForward) > 0 ? 1 : -1;
            
            return Vector3.Angle(worldUp, transformUp) * rezus;
        }
    }
}
