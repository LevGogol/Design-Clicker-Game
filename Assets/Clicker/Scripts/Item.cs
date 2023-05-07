using DG.Tweening;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int Index;
    
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _coinsPerSecond;
    [SerializeField] private int _cost;
    
    public Sprite Sprite => _sprite;
    public string Name => _name;
    public string Description => _description;
    public int CoinsPerSecond => _coinsPerSecond;
    public int Cost => _cost;
    public bool Buyed { get; set; }

    public void Show()
    {
        gameObject.SetActive(true);

        transform.DOScale(1f, 1f).SetEase(Ease.OutBounce).From(0f);
    }
}