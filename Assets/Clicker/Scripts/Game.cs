using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private int _coinsPerClick;
    [SerializeField] private int _coinsPerSecond;
    [SerializeField] private InputFacade _input;
    [SerializeField] private Screens _screens;
    [SerializeField] private CameraFacade _camera;
    [SerializeField] private Enviroment _enviroment;

    private Wallet _wallet;
    private CoinsPerSecondInfo _coinsPerSecondInfo;
    private int _buyedIndex;

    private void Awake()
    {
        _wallet = new Wallet();
        _coinsPerSecondInfo = new CoinsPerSecondInfo();
        _screens.CoinsUI.DrawCoins(_wallet.CoinCount);
        _screens.CoinsPerSecondUI.DrawCoinsPerSecond(_coinsPerSecondInfo.Get());
        _screens.Shop.AddItem(_enviroment.GetItem(0));

        StartCoroutine(UpdateCoinsPerSecond());
    }

    private void ShopOnItemBuyed(ItemUI itemUI)
    {
        _enviroment.GetItem(_buyedIndex).Show();
        _buyedIndex++;
        DOVirtual.DelayedCall(1f, () =>
        {
            _screens.Shop.AddItem(_enviroment.GetItem(_buyedIndex));
        });
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
        _input.DownTouched += OnInputDowned;
        _input.MouseDeltaChanged += OnMouseDeltaChanged;
        _screens.Shop.ItemBuyed += ShopOnItemBuyed;
    }

    private void OnMouseDeltaChanged(Vector2 direction)
    {
        var processDirection = new Vector3(-direction.x, 0, -direction.y);
        processDirection = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * processDirection;
        _camera.TryMove(processDirection);
    }

    private void OnDisable()
    {
        _input.DownTouched -= OnInputDowned;
        _input.MouseDeltaChanged -= OnMouseDeltaChanged;
        _screens.Shop.ItemBuyed -= ShopOnItemBuyed;
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