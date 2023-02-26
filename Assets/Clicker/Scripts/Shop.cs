using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ItemUI[] _items;
    
    public event Action<ItemUI> ItemBuyed;

    private void Start()
    {
        foreach (var item in _items)
        {
            item.Buyed += () => ItemBuyed?.Invoke(item);
        }
    }

    private void OnValidate()
    {
        _items = GetComponentsInChildren<ItemUI>();
    }
}