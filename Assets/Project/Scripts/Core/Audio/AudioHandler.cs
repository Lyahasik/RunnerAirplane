using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RunnerAirplane.Core.Audio
{
    public class AudioHandler : MonoBehaviour
    {
        [SerializeField] private List<AudioData> _sourcesMusic;
        
        [SerializeField] private List<AudioData> _sourcesBaseSounds;
        [SerializeField] private List<AudioData> _sourcesBattleSounds;
        
        public void SetMusicMute(bool value)
        {
            foreach (AudioData sourceMusic in _sourcesMusic)
            {
                sourceMusic.AudioSource.mute = value;
            }
        }
        
        public void SetSoundsMute(bool value)
        {
            foreach (AudioData sourceSound in _sourcesBaseSounds)
            {
                sourceSound.AudioSource.mute = value;
            }
            
            foreach (AudioData sourceSound in _sourcesBattleSounds)
            {
                sourceSound.AudioSource.mute = value;
            }
        }

        public void PlayMusic(AudioType type)
        {
            _sourcesMusic.First(data => data.AudioType == type).AudioSource.Play();
        }
        
        public void StopMusic(AudioType type)
        {
            _sourcesMusic.First(data => data.AudioType == type).AudioSource.Stop();
        }

        public void PlayBaseSound(AudioType type)
        {
            _sourcesBaseSounds.First(data => data.AudioType == type).AudioSource.Play();
        }

        public void StopBaseSound(AudioType type)
        {
            _sourcesBaseSounds.First(data => data.AudioType == type).AudioSource.Stop();
        }

        public void PlayBattleSound(AudioType type)
        {
            _sourcesBattleSounds.First(data => data.AudioType == type).AudioSource.Play();
        }

        public void StopBattleSound(AudioType type)
        {
            _sourcesBattleSounds.First(data => data.AudioType == type).AudioSource.Stop();
        }

        public void StopSoundAll()
        {
            foreach (AudioData audioData in _sourcesBaseSounds)
            {
                audioData.AudioSource.Stop();
            }
            
            foreach (AudioData audioData in _sourcesBattleSounds)
            {
                audioData.AudioSource.Stop();
            }
        }
    }
}
