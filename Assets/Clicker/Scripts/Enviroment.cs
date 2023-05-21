using UnityEngine;

public class Enviroment : MonoBehaviour
{
    [SerializeField] private Group[] _groups;
    public int GroupCount => _groups.Length;

    private void Awake()
    {
        var previousCount = 0;
        for (int x = 0; x < _groups.Length; x++)
        {
            for (var y = 0; y < _groups[x].Items.Length; y++)
            {
                var item = _groups[x].Items[y];
                item.Index = previousCount + y;
                item.gameObject.SetActive(false);
            }

            previousCount += _groups[x].Items.Length;
        }
    }

    public Group GetGroup(int index)
    {
        return _groups[index];
    }

    public Item GetItem(int index)
    {
        var previousItems = 0;
        
        for (int x = 0; x < _groups.Length; x++)
        {
            if (index >= _groups[x].Items.Length + previousItems)
            {
                previousItems += _groups[x].Items.Length;
                continue;
            }

            return _groups[x].Items[index - previousItems];
        }
        
        return null;
    }
    
    [ContextMenu("FillGroups")]
    public void FillGroups()
    {
        _groups = GetComponentsInChildren<Group>();
    }
}