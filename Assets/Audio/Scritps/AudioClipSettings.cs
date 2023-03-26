using System;
using UnityEngine;

namespace Azur.PlayableTemplate.Sound
{
    [Serializable]
    public class AudioClipSettings
    {
        public AudioClip Clip;
        
        [Range(0f, 1f)]
        public float Volume = 1f;
        
        [Range(0f, 3f)]
        public float Pitch = 1f;
        
        public AudioClipSettings(AudioClip clip = null)
        {
            Clip = clip;
        }
    }
}