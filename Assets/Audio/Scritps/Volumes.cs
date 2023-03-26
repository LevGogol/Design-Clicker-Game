using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azur.PlayableTemplate.Sound
{
    public class Volumes : MonoBehaviour
    {
        private Dictionary<string, Volume> _volumes = new Dictionary<string, Volume>();
        private Coroutine _changeVolumeCoroutine;

        public bool OnPause { get; set; }
        
        public void Initialize(string key)
        {
            AddVolume(key, new Volume(1f, 1f));
        }

        public void AddVolume(string key, Volume volume)
        {
            _volumes.Add(key, volume);
        }

        public Volume GetVolume(string key)
        {
            return _volumes[key];
        }
        
        public void SetVolume(string key, Volume newVolume)
        {
            _volumes[key] = newVolume;
        }

        public float ProcessMusicVolume(float start = 1f)
        {
            foreach (var volumeSetting in _volumes)
            {
                start *= volumeSetting.Value.Music;
            }

            return start;
        }
        
        public float ProcessSoundVolume(float startVolume = 1f)
        {
            foreach (var volumeSetting in _volumes)
            {
                startVolume *= volumeSetting.Value.Sound;
            }

            return startVolume;
        }

        public void ChangeVolumeSmooth(float endVolume, float duration, AudioSource source)
        {
            OnPause = endVolume <= 0;
            
            if (!OnPause)
                source.UnPause();
            
            if (_changeVolumeCoroutine != null)
            {
                StopCoroutine(_changeVolumeCoroutine);
                _changeVolumeCoroutine = null;
            }

            if (duration > 0f)
            {
                _changeVolumeCoroutine = StartCoroutine(Smooth(source, endVolume, duration));
            }
            else
            {
                source.volume = ProcessMusicVolume(endVolume);
            }
        }

        private IEnumerator Smooth(AudioSource source, float endVolume, float duration)
        {
            var startVolume = source.volume;
            var interpolant = 0f;

            while (interpolant < 1f)
            {
                source.volume = ProcessMusicVolume(Mathf.Lerp(startVolume, endVolume, interpolant));

                interpolant += Time.deltaTime / duration;
                yield return null;
            }

            if (OnPause)
                source.Pause();

            source.volume = ProcessMusicVolume(endVolume);
        }
    }
}