using UnityEngine;

public class Screens : MonoBehaviour
{
    [SerializeField] private CoinsUI _coinsUI;
    [SerializeField] private CoinsPerSecondUI _coinsPerSecondUI;
    [SerializeField] private CoinsPerClickUI _coinsPerClickUI;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Shop _shop;
    [SerializeField] private Popap _popap;

    public CoinsUI CoinsUI => _coinsUI;
    public CoinsPerSecondUI CoinsPerSecondUI => _coinsPerSecondUI;
    public CoinsPerClickUI CoinsPerClickUI => _coinsPerClickUI;
    public Shop Shop => _shop;
    public Canvas Canvas => _canvas;
    public Popap Popap => _popap;
}