using UnityEngine;

namespace RunnerAirplane.Gameplay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public void TakeStepMove(Vector3 step)
        {
            step *= Time.deltaTime;
            
            transform.Translate(step);
        }
    }
}
