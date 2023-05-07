using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PlusSystem : MonoBehaviour
{
    [SerializeField] private ItemPoint _itemPointPrefab;

    private List<ItemPoint> _itemPoints = new List<ItemPoint>();

    public event Action<int> ItemClicked = delegate {  };
        
    public void AddItem(Item item)
    {
        var itemPoint = Instantiate(_itemPointPrefab, transform);
        itemPoint.transform.position = item.transform.position;
        itemPoint.Initialize(item);
        itemPoint.Clicked += () => ItemClicked?.Invoke(item.Index);
        
        _itemPoints.Add(itemPoint);
    }

    public void Hide(int index)
    {
        _itemPoints[index].Hide();
    }

    public void AddItemWithAnimation(Item item)
    {
        AddItem(item);
        _itemPoints.Last().transform.DOScale(1f, 0.2f).From(0f);
    }
}