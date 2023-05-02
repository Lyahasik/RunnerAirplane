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
        
        public void StartDemoObstaclesScene()
        {
            SceneManager.LoadScene("DemoObstaclesScene");
        }
        
        public void StartDemoGatesScene()
        {
            SceneManager.LoadScene("DemoGatesScene");
        }
        
        public void StartDemoCombatScene()
        {
            SceneManager.LoadScene("DemoCombatScene");
        }

        public void StartIndexScene(int index)
        {
            SceneManager.LoadScene(index);
        }
    }
}
