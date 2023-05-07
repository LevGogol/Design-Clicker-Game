using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private ItemUI _itemUIPrefab;
    [SerializeField] private RectTransform _contentRoot;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
    [SerializeField] private HideButton _hideButton;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _endValue;
    [SerializeField] private float _duration;

    private bool _isHide;
    private float _startValue;
    private List<ItemUI> _itemsUi = new List<ItemUI>();
    private Vector2 _startSize;
    private Canvas _canvas;
    private bool _isDrag;
    private Vector3 _lastPosition;
    private Tween _tween;

    public event Action<int> ItemClicked;

    private void Awake()
    {
        _canvas = GetComponentInParent<Canvas>();
        _startSize = _rectTransform.sizeDelta;
    }

    private void OnEnable()
    {
        _hideButton.PointerDowned += OnPointerDowned;
        _hideButton.PointerUpped += OnPointerUpped;
    }

    private void OnDisable()
    {
        _hideButton.PointerDowned -= OnPointerDowned;
        _hideButton.PointerUpped -= OnPointerUpped;
    }

    private void Start()
    {
        _startValue = _rectTransform.anchoredPosition.y;
        _startSize = _rectTransform.sizeDelta;
    }

    private void Update()
    {
        if(_isDrag == false)
            return;

        Vector2 delta = Vector2.zero;
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            _lastPosition = Input.mousePosition;
        }
        if (UnityEngine.Input.GetMouseButton(0))
        {
            delta = (Input.mousePosition - _lastPosition);
            _lastPosition = Input.mousePosition;
        }

        if (UnityEngine.Input.GetMouseButtonUp(0))
        {
            delta = Vector2.zero;
        }
        
        var rectTransformAnchoredPosition = _rectTransform.anchoredPosition;
        rectTransformAnchoredPosition.y += delta.y / _canvas.scaleFactor;
        _rectTransform.anchoredPosition = rectTransformAnchoredPosition;
        
        var sizeDelta = _rectTransform.sizeDelta;
        sizeDelta.y = _rectTransform.anchoredPosition.y;
        _rectTransform.sizeDelta = sizeDelta;
    }

    private void OnPointerDowned()
    {
        _isDrag = true;
    }

    private void OnPointerUpped()
    {
        OnHideButtonClicked();
        _isDrag = false;
    }

    private void OnHideButtonClicked()
    {
        if(_isHide)
            Show();
        else
            Hide();

        _isHide = !_isHide;
    }

    private void Show()
    {
        _rectTransform.sizeDelta = _startSize;
        _rectTransform.DOAnchorPosY(_startValue, _duration);
    }

    public void HideNoAnimation()
    {
        var position = _rectTransform.anchoredPosition;
        position.y = _endValue;
        _rectTransform.anchoredPosition = position;
    }

    public void Hide()
    {
        _rectTransform.DOAnchorPosY(_endValue, _duration);
    }

    public ItemUI AddItem(Item item)
    {
        var size = _contentRoot.sizeDelta;
        size.y += 250;
        _contentRoot.sizeDelta = size;
        
        var itemUI = Instantiate(_itemUIPrefab, _contentRoot);
        itemUI.transform.SetSiblingIndex(0);
        itemUI.Initialize(item);
        itemUI.Clicked += () => ItemClicked?.Invoke(item.Index);
        
        _itemsUi.Add(itemUI);

        return itemUI;
    }

    public void AddItemWithAnimation(Item item)
    {
        var itemUI = AddItem(item);
        itemUI.transform.DOScale(1f, 1f).From(0.2f).SetEase(Ease.OutSine);

    }

    public void BuyItem(int index)
    {
        _itemsUi[index].Deactivate();
    }
}