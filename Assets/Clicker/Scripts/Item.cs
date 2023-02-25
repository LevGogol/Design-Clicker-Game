using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Item : MonoBehaviour
{
    [SerializeField] private Button _button;

    public event Action Buyed;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Buyed?.Invoke();
    }
}