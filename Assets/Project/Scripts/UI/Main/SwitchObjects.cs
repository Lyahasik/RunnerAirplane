using UnityEngine;

namespace RunnerAirplane.UI.Main
{
    public class SwitchObjects : MonoBehaviour
    {
        [SerializeField] private GameObject _closing;
        [SerializeField] private GameObject _opening;

        public void Switch()
        {
            _closing.SetActive(false);
            _opening.SetActive(true);
        }
    }
}
