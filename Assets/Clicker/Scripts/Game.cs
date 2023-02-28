using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    [SerializeField] private int _coins;
    [SerializeField] private int _coinsPerClick;
    [SerializeField] private int _coinsPerSecond;
    [SerializeField] private InputFacade _input;
    [SerializeField] private Screens _screens;
    [SerializeField] private CameraFacade _camera;
    [SerializeField] private Enviroment _enviroment;

    [SerializeField] private float _minRandomOffset;
    [SerializeField] private float _maxRandomOffset;
    [SerializeField] private float _upOffset;

    private Wallet _wallet;
    private CoinsPerSecondInfo _coinsPerSecondInfo;
    private int _buyedIndex;

    private void Awake()
    {
        _wallet = new Wallet();
        _wallet.CoinCount = _coins;
        _coinsPerSecondInfo = new CoinsPerSecondInfo();
        _screens.CoinsUI.ForceDrawCoins(_wallet.CoinCount);
        _screens.CoinsPerSecondUI.DrawCoinsPerSecond(_coinsPerSecondInfo.Get());
        _screens.Shop.AddItem(_enviroment.GetItem(0));

        StartCoroutine(UpdateCoinsPerSecond());
    }

    private void OnEnable()
    {
        // _input.DownTouched += OnInputDowned;
        _input.MouseDeltaChanged += OnMouseDeltaChanged;
        _screens.Shop.ItemClicked += ShopOnItemClicked;
    }

    private void ShopOnItemClicked(ItemUI itemUI)
    {
        if(itemUI.ItemOnScene.Cost > _wallet.CoinCount)
            return;

        _wallet.CoinCount -= itemUI.ItemOnScene.Cost;
        _screens.CoinsUI.DrawCoins(_wallet.CoinCount);

        itemUI.Deactivate();

        var sequence = DOTween.Sequence();
        sequence.AppendCallback(() => _camera.MoveTo(itemUI.ItemOnScene.transform.position));
        sequence.AppendInterval(0.5f);
        sequence.AppendCallback(() => itemUI.ItemOnScene.Show());
        sequence.AppendInterval(1f);
        sequence.AppendCallback(() => _screens.Shop.AddItemWithAnimation(_enviroment.GetItem(_buyedIndex)));
            
        _buyedIndex++;
    }

    private IEnumerator UpdateCoinsPerSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            AddCoins(_coinsPerSecond);
            _screens.CoinsPerSecondUI.DrawCoinsPerSecond(_coinsPerSecondInfo.Get());
        }
    }

    private void OnMouseDeltaChanged(Vector2 direction)
    {
        var processDirection = new Vector3(-direction.x, 0, -direction.y);
        // processDirection = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * processDirection;
        _camera.Move(processDirection);
    }

    private void OnDisable()
    {
        _input.DownTouched -= OnInputDowned;
        _input.MouseDeltaChanged -= OnMouseDeltaChanged;
        _screens.Shop.ItemClicked -= ShopOnItemClicked;
    }

    private void OnInputDowned()
    {
        AddCoins(_coinsPerClick);
        DrawCoinPerClick();
    }

    private void DrawCoinPerClick()
    {
        var randomOffset = new Vector3(Random.Range(_minRandomOffset, _maxRandomOffset),
            Random.Range(0, _maxRandomOffset) + _upOffset);
        _screens.CoinsPerClickUI.DrawCoinsPerClick(_coinsPerClick,
            Input.mousePosition / _screens.Canvas.scaleFactor + randomOffset);
    }

    private void AddCoins(int coins)
    {
        _wallet.CoinCount += coins;
        _coinsPerSecondInfo.Registrate(coins);
        _screens.CoinsUI.DrawCoins(_wallet.CoinCount);
    }
}