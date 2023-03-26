using UnityEngine;

public class Enviroment : MonoBehaviour
{
    [SerializeField] private Item[] _items;

    private void Awake()
    {
        for (var index = 0; index < _items.Length; index++)
        {
            var item = _items[index];
            item.Index = index;
            item.gameObject.SetActive(false);
        }
    }

    public Item GetItem(int index)
    {
        return _items[index];
    }
}