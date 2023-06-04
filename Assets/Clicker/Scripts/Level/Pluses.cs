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
        var scale = transform.localScale;
        transform.parent = null;
        transform.localScale = scale;
    }

    public void AddPlus(Item item)
    {
        var plusItem = Instantiate(plusItemPrefab, transform);
        plusItem.transform.position = item.transform.position;
        plusItem.transform.rotation = Quaternion.identity;

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
        plus.transform.DOScale(plus.transform.localScale, 0.2f).From(0f).OnComplete(plus.Pulse);
    }
}