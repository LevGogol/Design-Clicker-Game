using System.Collections.Generic;
using UnityEngine;

namespace Azur.PlayableTemplate.Sound
{
    [RequireComponent(typeof(Sources))]
    public class Audio : MonoBehaviour
    {
        [HideInInspector] public List<AudioTrack> Tracks = new List<AudioTrack>();

        private AudioClipSettings _currentMusicTrack;
        private AudioClipSettings _currentSoundTrack;
        private AudioClipSettings _currentSoundOneShotTrack;
        private List<AudioClipSettings> _currentLoopSounds = new List<AudioClipSettings>();
        private Sources _sources;
        private static Audio _instance;

        public static Audio Instance => _instance;
        public void Mute() => _sources.Mute();
        public void UnMute() => _sources.UnMute();
        public void AddVolumeSettings(string key, Volume volume) => _sources.AddVolume(key, volume);

        public void SetMusicVolume(float volume)
        {
            if(_currentMusicTrack == null)
                return;
            
            _sources.SetMusicVolume(volume, _currentMusicTrack.Volume);
        }
        
        public void SetSoundVolume(float volume)
        {
            _sources.SetSoundVolume(volume, _currentSoundTrack?.Volume ?? 1, _currentLoopSounds);
        }

        private void Awake()
        {
            _instance = this;
            _sources = GetComponent<Sources>();
        }

        private void Start()
        {
            _sources.SoundSource.transform.SetParent(transform);
            _sources.SoundSourceOneShot.transform.SetParent(transform);
            _sources.MusicSource.transform.SetParent(transform);
            _sources.MusicSource.loop = true;
        }

        public void PlaySound(TrackName clipKey)
        {
            var track = Tracks[(int)clipKey];
            _currentSoundTrack = track.GetRandomAudioClipSettings();
            var source = _sources.SoundSource;

            source.Stop();
            source.pitch = _currentSoundTrack.Pitch;
            source.volume = _sources.ProcessSoundVolume(_currentSoundTrack.Volume);
            source.clip = _currentSoundTrack.Clip;
            source.Play();
        }

        public void PlaySoundOneShot(TrackName clipKey)
        {
            var track = Tracks[(int)clipKey];
            var source = _sources.SoundSourceOneShot;
            _currentSoundOneShotTrack = track.GetRandomAudioClipSettings();

            source.pitch = _currentSoundOneShotTrack.Pitch;
            source.volume = _sources.ProcessSoundVolume(_currentSoundOneShotTrack.Volume);
            source.PlayOneShot(_currentSoundOneShotTrack.Clip);
        }

        public void PlaySoundLoop(TrackName clipKey)
        {
            var track = Tracks[(int)clipKey];
            var clipSettings = track.GetRandomAudioClipSettings();
            _currentLoopSounds.Add(clipSettings);

            var source = _sources.GetLoopSource(track.Name);
            source.transform.SetParent(transform);
            source.pitch = clipSettings.Pitch;
            source.volume = _sources.ProcessSoundVolume(clipSettings.Volume);
            source.clip = clipSettings.Clip;
            source.loop = true;
            source.Play();
        }

        public void StopSoundLoop(TrackName clipKey)
        {
            AudioSource loopAudioSource = _sources.GetLoopSource(clipKey);

            loopAudioSource.Stop();
        }

        public void PlayMusic(TrackName musicKey, float smoothTime = 0f)
        {
            var track = Tracks[(int)musicKey];

            if (track.ClipsSettings.Length != 0)
            {
                var musicSource = _sources.MusicSource;
                _currentMusicTrack = track.GetRandomAudioClipSettings();
                musicSource.pitch = _currentMusicTrack.Pitch;
                musicSource.clip = _currentMusicTrack.Clip;
                musicSource.volume = 0;
                musicSource.Play();

                _sources.ChangeVolumeSmooth(_currentMusicTrack.Volume, smoothTime);
            }
            else
            {
                Debug.LogError("Zero tracks in Audio Editor. Fill tracks, please");
            }
        }

        public void PauseMusic(float smoothTime = 0f)
        {
            _sources.ChangeVolumeSmooth(0, smoothTime);
        }

        public void ContinueMusic(float smoothTime = 0f)
        {
            _sources.ChangeVolumeSmooth(_currentMusicTrack.Volume, smoothTime);
        }

        private void OnDestroy()
        {
            _instance = null;
        }
    }
}