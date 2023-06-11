using System.Collections;
using Azur.PlayableTemplate.Sound;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private ClipSource _clipSource;
    
    public void OnPress()
    {
        StartCoroutine(OnPressCoroutine());
    }

    public IEnumerator OnPressCoroutine()
    {
        yield return null;
        
        _clipSource.Play();
    }
}
