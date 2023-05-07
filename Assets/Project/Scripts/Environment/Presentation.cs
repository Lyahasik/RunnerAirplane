using System.Linq;
using UnityEngine;

using RunnerAirplane.Gameplay.Progress;
using RunnerAirplane.ScriptableObjects;

namespace RunnerAirplane.Environment
{
    public class Presentation : MonoBehaviour
    {
        [SerializeField] private ListSelectedEraData _listSelectedEra;
        [SerializeField] private float _angleRotationX;

        private GameObject _currentSkin;

        private void OnEnable()
        {
            if (_currentSkin)
                Destroy(_currentSkin);

            int health = ProcessingProgress.GetLastStartHealth();
            GameObject prefab = _listSelectedEra.ListEra
                .First(data => health >= data.MinValue && health <= data.MaxValue)
                .Prefab;
            
            _currentSkin = Instantiate(prefab, transform.position, Quaternion.Euler(0f, _angleRotationX, 0f));
            _currentSkin.transform.parent = transform;
        }
    }
}
