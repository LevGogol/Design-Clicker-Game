using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlusItem : MonoBehaviour
{
    [SerializeField] private float _pulseSpeed;
    [SerializeField] private Vector3 _downScale;
    [SerializeField] private float _downDurationScale;
    [SerializeField] private Vector3 _upScale;
    [SerializeField] private float _upDurationScale;
    [SerializeField] private SpriteRenderer _backgroundSprite;
    [SerializeField] private Color _backgroundPressedColor;
    [SerializeField] private SpriteRenderer _iconSprite;
    [SerializeField] private Color _iconPressedColor;

    private Sequence _sequence;
    private Color _backgroundUpColor;
    private Color _iconUpColor;
    private bool _isClicked;

    public event Action Clicked = delegate { };

    private void Awake()
    {
        _backgroundUpColor = _backgroundSprite.color;
        _iconUpColor = _iconSprite.color;
    }

    private void OnMouseDown()
    {
        transform.DOKill();

        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOScale(_downScale, _downDurationScale));
        _sequence.Join(_backgroundSprite.DOColor(_backgroundPressedColor, _downDurationScale));
        _sequence.Join(_iconSprite.DOColor(_iconPressedColor, _downDurationScale));
    }

    private void OnMouseUp()
    {
        if(_isClicked)
            return;

        _isClicked = true;
        
        Clicked.Invoke();
    }

    public void Pulse()
    {
        var endScale = transform.localScale * 0.8f;
        transform.DOScale(endScale, _pulseSpeed).SetLoops(-1, LoopType.Yoyo);
    }

    public void Hide()
    {
        if (_sequence.active)
        {
            _sequence.OnComplete(() =>
            {
                transform.DOScale(_upScale, _upDurationScale).OnComplete(() => gameObject.SetActive(false));
                _backgroundSprite.DOColor(_backgroundUpColor, _downDurationScale);
                _iconSprite.DOColor(_iconUpColor, _downDurationScale);
            });
        }
        else
        {
            transform.DOScale(_upScale, _upDurationScale).OnComplete(() => gameObject.SetActive(false));
            _backgroundSprite.DOColor(_backgroundUpColor, _downDurationScale);
            _iconSprite.DOColor(_iconUpColor, _downDurationScale);
        }
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
            transform.DOKill();

            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOScale(_downScale, _downDurationScale));
            _sequence.Join(_backgroundSprite.DOColor(_backgroundPressedColor, _downDurationScale));
            _sequence.Join(_iconSprite.DOColor(_iconPressedColor, _downDurationScale));
            Clicked.Invoke();
        }
    }
#endif
}