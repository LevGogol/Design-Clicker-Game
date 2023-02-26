using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ItemUI _itemUIPrefab;
    [SerializeField] private RectTransform _contentRoot;
    
    private List<ItemUI> _items = new List<ItemUI>();
    
    public event Action<ItemUI> ItemBuyed;

    public void AddItem(Item item)
    {
        var itemUI = Instantiate(_itemUIPrefab, _contentRoot);
        itemUI.Initialize(item);
        itemUI.Buyed += () => ItemBuyed?.Invoke(itemUI);

        itemUI.RectTransform.DOScale(1f, 1f).From(0);
    }
}