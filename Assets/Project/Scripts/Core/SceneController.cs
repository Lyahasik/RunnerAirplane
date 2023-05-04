using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunnerAirplane.Core
{
    public class SceneController : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void StartMainScene()
        {
            SceneManager.LoadScene("MainScene");
        }

        public void StartIndexScene(int index)
        {
            SceneManager.LoadScene(index * 5 - 4);
        }

        public static void PreviousScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        public static void NextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
