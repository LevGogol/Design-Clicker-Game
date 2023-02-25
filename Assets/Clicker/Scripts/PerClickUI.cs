using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PerClickUI : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private Animation _animation;

    public event Action AnimationEnded;
    
    public RectTransform RectTransform => _rectTransform;
    public TextMeshProUGUI TextMesh => _textMesh;
    
    private void OnEnable()
    {
        DOVirtual.DelayedCall(_animation.clip.length, OnAnimationEnded);
    }

    private void OnAnimationEnded()
    {
        AnimationEnded?.Invoke();
    }
}