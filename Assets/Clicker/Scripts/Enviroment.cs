using System;
using UnityEngine;

public class Enviroment : MonoBehaviour
{
    [SerializeField] private Item[] _items;

    private void Awake()
    {
        foreach (var item in _items)
        {
            item.gameObject.SetActive(false);
        }
    }

    public Item GetItem(int index)
    {
        return _items[index];
    }
}