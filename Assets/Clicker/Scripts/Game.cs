using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private int _coinsPerClick;
    [SerializeField] private int _coinsPerSecond;
    [SerializeField] private InputFacade _input;
    [SerializeField] private Screens _screens;

    private Wallet _wallet;
    private CoinsPerSecondInfo _coinsPerSecondInfo;

    private void Awake()
    {
        _wallet = new Wallet();
        _coinsPerSecondInfo = new CoinsPerSecondInfo();
        _screens.CoinsUI.DrawCoins(_wallet.CoinCount);
        _screens.CoinsPerSecondUI.DrawCoinsPerSecond(_coinsPerSecondInfo.Get());

        StartCoroutine(UpdateCoinsPerSecond());
    }

    private void Start()
    {
        _screens.Shop.ItemBuyed += ShopOnItemBuyed;
    }

    private void ShopOnItemBuyed(ItemUI itemUI)
    {
        print(itemUI.name);
    }

    private IEnumerator UpdateCoinsPerSecond()
    {
        while (true)
        {
            AddCoins(_coinsPerSecond);
            _screens.CoinsPerSecondUI.DrawCoinsPerSecond(_coinsPerSecondInfo.Get());
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnEnable()
    {
        _input.Downed += OnInputDowned;
    }

    private void OnInputDowned()
    {
        AddCoins(_coinsPerClick);
        _screens.CoinsPerClickUI.DrawCoinsPerClick(_coinsPerClick, Input.mousePosition / _screens.Canvas.scaleFactor);
    }

    private void AddCoins(int coins)
    {
        _wallet.CoinCount += coins;
        _coinsPerSecondInfo.Registrate(coins);
        _screens.CoinsUI.DrawCoins(_wallet.CoinCount);
    }
}