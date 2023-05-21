using UnityEditor;
using UnityEngine;

public class Group : MonoBehaviour
{
    [SerializeField] private Item[] _items;

    public Item[] Items => _items;

    #if UNITY_EDITOR
    [ContextMenu("FillItems")]
    public void FillItems()
    {
        _items = GetComponentsInChildren<Item>();
    }
    #endif
}