using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Popap : MonoBehaviour
{
    [SerializeField] private Button _close;
    [SerializeField] private Button _ok;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _duration;

    public event Action Applyed;

    private void OnEnable()
    {
        _close.onClick.AddListener(Close);
        _ok.onClick.AddListener(Apply);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        transform.DOScale(1f, _duration).From(0).SetEase(_animationCurve);
    }

    private void Apply()
    {
        gameObject.SetActive(false);
        Applyed?.Invoke();
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }
}
