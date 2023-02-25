using System;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPerClickUI : MonoBehaviour
{
    [SerializeField] private PerClickUI _perClickPrefab;

    private const int _poolSize = 20;
    private Queue<PerClickUI> _queue = new Queue<PerClickUI>();

    private void Awake()
    {
        _perClickPrefab.gameObject.SetActive(false);
        for (int i = 0; i < _poolSize; i++)
        {
            var perClick = Instantiate(_perClickPrefab, transform);
            perClick.gameObject.SetActive(false);
            perClick.AnimationEnded += () => AddToQueue(perClick);
            _queue.Enqueue(perClick);
        }
    }

    public void DrawCoinsPerClick(int value, Vector2 position)
    {
        if (_queue.Count == 0)
            return;
        
        var perClick = _queue.Dequeue();
        perClick.gameObject.SetActive(true);
        perClick.RectTransform.anchoredPosition = position;
        perClick.TextMesh.text = value.ToString();
    }

    private void AddToQueue(PerClickUI perClick)
    {
        perClick.gameObject.SetActive(false);
        _queue.Enqueue(perClick);
    }
}