using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Item[] _items;
    
    public event Action<Item> ItemBuyed;

    private void Start()
    {
        foreach (var item in _items)
        {
            item.Buyed += () => ItemBuyed?.Invoke(item);
        }
    }

    private void OnValidate()
    {
        _items = GetComponentsInChildren<Item>();
    }
}