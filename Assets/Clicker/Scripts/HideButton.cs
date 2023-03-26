using System;
using Azur.PlayableTemplate.Sound;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HideButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Button _button;
    [SerializeField] private ClipSource _clipSource;
    
    public event Action PointerDowned;
    public event Action PointerUpped;

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDowned.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _button.onClick.Invoke();
        _clipSource.Play();
        PointerUpped.Invoke();
    }
}