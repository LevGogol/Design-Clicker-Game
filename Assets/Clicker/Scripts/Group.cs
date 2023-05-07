using UnityEngine;

public class Group : MonoBehaviour
{
    [SerializeField] private Item[] _items;

    public Item[] Items => _items;

    [ContextMenu("FillItems")]
    public void FillItems()
    {
        _items = GetComponentsInChildren<Item>();
    }
}