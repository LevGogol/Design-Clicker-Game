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
    
    private List<ItemUI> _items = new List<ItemUI>();
    
    public event Action<ItemUI> ItemBuyed;

    public ItemUI AddItem(Item item)
    {
        var itemUI = Instantiate(_itemUIPrefab, _contentRoot);
        itemUI.transform.SetSiblingIndex(0);
        itemUI.Initialize(item);
        itemUI.Buyed += () => ItemBuyed?.Invoke(itemUI);

        return itemUI;
    }
    
    public void AddItemWithAnimation(Item item)
    {
        _scrollRect.enabled = false;
        
        _contentRoot.DOAnchorPosY(-250, 1f).OnComplete(() =>
        {
            _verticalLayoutGroup.enabled = false;
            
            var itemUI = AddItem(item);
            itemUI.RectTransform.anchoredPosition = Vector3.up * 125;
            itemUI.RectTransform.localScale = Vector3.zero;

            itemUI.RectTransform.DOScale(1f, 1f).OnComplete(() =>
            {
                _scrollRect.enabled = true;
                _verticalLayoutGroup.enabled = true;
            });
        });
        
    }
}