using System;
using DG.Tweening;
using UnityEngine;

public class PlusItem : MonoBehaviour
{
    [SerializeField] private float _endScale;
    [SerializeField] private float _duration;
    
    public event Action Clicked = delegate { };

    private void OnMouseUp()
    {
        Clicked.Invoke();
    }

    public void Pulse()
    {
        transform.DOScale(_endScale, _duration).SetLoops(-1, LoopType.Yoyo);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}