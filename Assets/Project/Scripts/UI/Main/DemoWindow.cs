using UnityEngine;

using RunnerAirplane.Core;

namespace RunnerAirplane
{
    public class DemoWindow : MonoBehaviour
    {
        private SceneController _sceneController;

        private void Start()
        {
            _sceneController = FindObjectOfType<SceneController>();
        }

        public void StartLevel(int index)
        {
            _sceneController.StartLevel(index);
        }

        public void StartIndexScene(int index)
        {
            _sceneController.StartIndexScene(index);
        }
    }
}
