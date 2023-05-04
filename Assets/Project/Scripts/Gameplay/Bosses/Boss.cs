using UnityEngine;

namespace RunnerAirplane.Gameplay.Bosses
{
    public abstract class Boss : MonoBehaviour
    {
        protected void OnEnable()
        {
            StartBattle();
        }

        protected void OnDisable()
        {
            EndBattle();
        }
        
        public abstract void StartBattle();
        public abstract void EndBattle();
    }
}
