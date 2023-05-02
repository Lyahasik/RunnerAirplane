using UnityEngine;

namespace RunnerAirplane.Gameplay.Bosses
{
    public abstract class Boss : MonoBehaviour
    {
        public abstract void StartBattle();
        public abstract void EndBattle();
    }
}
