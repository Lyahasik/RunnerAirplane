using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunnerAirplane.Core
{
    public class SceneController : MonoBehaviour
    {
        private const int _numberLevels = 50;
        
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void StartLevel(int index)
        {
            SceneManager.LoadScene(index * 5 - 4);
        }

        public void StartIndexScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public static void PreviousScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        public static void NextLevel()
        {
            int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            
            if (nextIndex <= _numberLevels)
                SceneManager.LoadScene(nextIndex);
        }
    }
}
