using System;
using DG.Tweening;
using UnityEngine;

public class PlusItem : MonoBehaviour
{
    [SerializeField] private float _duration;
    
    public event Action Clicked = delegate { };

    private void OnMouseUp()
    {
        Clicked.Invoke();
    }

    public void Pulse()
    {
        var endScale = transform.localScale * 0.8f;
        transform.DOScale(endScale, _duration).SetLoops(-1, LoopType.Yoyo);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
           Clicked.Invoke(); 
        }
    }
#endif
}