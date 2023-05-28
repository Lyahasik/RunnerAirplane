using UnityEngine;

namespace RunnerAirplane.UI.Main
{
    public class SwitchObjects : MonoBehaviour
    {
        [SerializeField] private GameObject _closing;
        [SerializeField] private GameObject _opening;

        public void Switch()
        {
            if (_closing)
                _closing.SetActive(false);
            if (_opening)
                _opening.SetActive(true);
        }
    }
}
