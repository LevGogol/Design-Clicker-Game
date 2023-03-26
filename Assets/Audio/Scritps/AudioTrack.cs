using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Azur.PlayableTemplate.Sound
{
    [Serializable]
    public class AudioTrack
    {
        [HideInInspector] public string ID;
        [HideInInspector] public TrackName Name;
        
        public AudioClipSettings[] ClipsSettings;
        
        public AudioClipSettings GetRandomAudioClipSettings()
        {
            return ClipsSettings[Random.Range(0, ClipsSettings.Length)];
        }
    }
}