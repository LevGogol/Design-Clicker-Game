using System.Collections.Generic;
using UnityEngine;

namespace Azur.PlayableTemplate.Sound
{
    [RequireComponent(typeof(Volumes))]
    public class Sources : MonoBehaviour
    {
        private Volumes _volumes;

        private const string _gameVolume = "GameVolume";

        public Dictionary<TrackName, AudioSource> LoopAudioSourcesMap { get; private set; }
        public AudioSource SoundSource { get; private set; }
        public AudioSource SoundSourceOneShot { get; private set; }
        public AudioSource MusicSource { get; private set; }
        public void AddVolume(string key, Volume volume) => _volumes.AddVolume(key, volume);
        public float ProcessSoundVolume(float start) => _volumes.ProcessSoundVolume(start);
        public float ProcessMusicVolume(float start) => _volumes.ProcessMusicVolume(start);

        public void ChangeVolumeSmooth(float endVolume, float duration) =>
            _volumes.ChangeVolumeSmooth(endVolume, duration, MusicSource);
        
        public void Awake()
        {
            _volumes = GetComponent<Volumes>();
            _volumes.Initialize(_gameVolume);

            LoopAudioSourcesMap = new Dictionary<TrackName, AudioSource>();

            SoundSource = new GameObject(nameof(SoundSource)).AddComponent<AudioSource>();
            SoundSource.playOnAwake = false;
            
            SoundSourceOneShot = new GameObject(nameof(SoundSourceOneShot)).AddComponent<AudioSource>();
            SoundSourceOneShot.playOnAwake = false;

            MusicSource = new GameObject(nameof(MusicSource)).AddComponent<AudioSource>();
            MusicSource.playOnAwake = false;
        }

        public void SetMusicVolume(float value, float clipVolume)
        {
            _volumes.SetVolume(_gameVolume, new Volume(value, _volumes.GetVolume(_gameVolume).Sound));
            MusicSource.volume = ProcessMusicVolume(clipVolume);
        }

        public void SetSoundVolume(float value, float clipVolume, List<AudioClipSettings> currentLoopSounds)
        {            
            var index = 0;

            _volumes.SetVolume(_gameVolume, new Volume(_volumes.GetVolume(_gameVolume).Music, value));
                
            SoundSource.volume = ProcessSoundVolume(clipVolume);
            
            foreach (var sourcePair in LoopAudioSourcesMap)
            {
                sourcePair.Value.volume = ProcessSoundVolume(currentLoopSounds[index].Volume);
                index++;
            }
        }

        public void Mute()
        {
            MusicSource.mute = true;
            SoundSource.mute = true;
            SoundSourceOneShot.mute = true;

            foreach (var sourcePair in LoopAudioSourcesMap)
            {
                sourcePair.Value.mute = true;
            }
        }

        public void UnMute()
        {
            MusicSource.mute = false;
            SoundSource.mute = false;
            SoundSourceOneShot.mute = false;

            foreach (var sourcePair in LoopAudioSourcesMap)
            {
                sourcePair.Value.mute = false;
            }
        }

        public AudioSource GetLoopSource(TrackName name)
        {
            if (LoopAudioSourcesMap.ContainsKey(name))
            {
                return LoopAudioSourcesMap[name];
            }

            var source = new GameObject(name + "_LoopSource").AddComponent<AudioSource>();
            source.playOnAwake = false;

            LoopAudioSourcesMap.Add(name, source);

            return source;
        }
        
    }
}