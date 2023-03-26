using System;
using UnityEngine;

public class ItemPoint : MonoBehaviour
{
    private Item _itemOnScene;
    
    public event Action Clicked = delegate { };
    
    public Item ItemOnScene => _itemOnScene;

    private void OnMouseUp()
    {
        Clicked.Invoke();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(Item item)
    {
        _itemOnScene = item;
    }
}