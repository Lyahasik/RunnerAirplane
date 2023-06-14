using UnityEngine;
using AudioType = RunnerAirplane.Core.Audio.AudioType;

namespace RunnerAirplane.Gameplay
{
    public class Technique : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private AudioType _audioType;

        public int Id => _id;
        public AudioType AudioType => _audioType;
    }
}
