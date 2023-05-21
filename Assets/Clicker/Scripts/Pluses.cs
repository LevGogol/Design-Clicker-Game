using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Pluses : MonoBehaviour
{
    [SerializeField] private PlusItem plusItemPrefab;

    private List<PlusItem> _plusItems = new List<PlusItem>();

    public event Action<Item> PlusClicked = delegate {  };

    private void Awake()
    {
        transform.parent = null;
        transform.localScale = Vector3.one;
    }

    public void AddPlus(Item item)
    {
        var plusItem = Instantiate(plusItemPrefab, transform);
        plusItem.transform.position = item.transform.position;
        plusItem.transform.rotation = Quaternion.identity;
       
        // Transform parent = plusItem.transform.parent;
        // plusItem.transform.SetParent(null);
        // plusItem.transform.localScale = Vector3.one;
        // plusItem.transform.SetParent(parent, true);
        
        plusItem.Clicked += () => PlusClicked?.Invoke(item);
        
        _plusItems.Add(plusItem);
    }

    public void Hide(int index)
    {
        _plusItems[index].Hide();
    }

    public void AddPlusWithAnimation(Item item)
    {
        AddPlus(item);
        var plus = _plusItems.Last();
        plus.transform.DOScale(1f, 0.2f).From(0f).OnComplete(plus.Pulse);
    }
}