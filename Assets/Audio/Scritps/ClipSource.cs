using UnityEngine;

namespace Azur.PlayableTemplate.Sound
{
    public class ClipSource : MonoBehaviour
    {
        [SerializeField] private TrackName _track;
        public void Play()
        {
            Audio.Instance.PlaySoundOneShot(_track);
        }
    }
}
