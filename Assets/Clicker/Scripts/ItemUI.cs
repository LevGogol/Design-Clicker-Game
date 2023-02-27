using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _coinsPerSecond;
    [SerializeField] private TextMeshProUGUI _cost;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Color _buttonDeactivate;

    private Item _itemOnScene;

    public event Action Buyed;

    public RectTransform RectTransform => _rectTransform;
    public Item ItemOnScene => _itemOnScene;

    private void Awake()
    {
        _button.onClick.AddListener(OnClick);
    }

    public void Initialize(Item item)
    {
        _image.sprite = item.Sprite;
        _name.text = item.Name;
        _description.text = item.Description;
        _cost.text = item.Cost + "₽";
        _coinsPerSecond.text = item.CoinsPerSecond + "₽/сек";
        _itemOnScene = item;
    }

    public void Deactivate()
    {
        _button.interactable = false;
        _buttonImage.color = _buttonDeactivate;
        _cost.text = "куплено";
    }

    private void OnClick()
    {
        Buyed?.Invoke();
    }
}