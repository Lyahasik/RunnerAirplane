using UnityEngine;

using RunnerAirplane.Gameplay.Progress;

namespace RunnerAirplane.Core
{
    public class StartSceneHandler : MonoBehaviour
    {
        private const int _firstLevelIndex = 1;

        [SerializeField] private SceneController _sceneController;

        private void Start()
        {
            int index = ProcessingProgress.GetLastLevel() + 1;

            if (index < _firstLevelIndex)
                index = _firstLevelIndex;
                
            _sceneController.StartIndexScene(index);
        }
    }
}
