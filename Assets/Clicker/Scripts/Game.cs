using System.Collections;
using Azur.PlayableTemplate.Sound;
using DG.Tweening;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private int _coinsPerSecond;
    [SerializeField] private InputFacade _input;
    [SerializeField] private Screens _screens;
    [SerializeField] private CameraFacade _camera;
    [SerializeField] private Enviroment _enviroment;
    [SerializeField] private Audio _audio;
    [SerializeField] private PlusSystem _plusSystem;

    private Wallet _wallet;
    private CoinsPerSecondInfo _coinsPerSecondInfo;
    private int _buyedIndex;

    private void Awake()
    {
        _wallet = new Wallet();
        _coinsPerSecondInfo = new CoinsPerSecondInfo();
        _screens.CoinsUI.ForceDrawCoins(_wallet.CoinCount);
        _screens.CoinsPerSecondUI.DrawCoinsPerSecond(_coinsPerSecondInfo.Get());
        _screens.Popap.Show();

        StartCoroutine(UpdateCoinsPerSecond());
    }

    private void OnEnable()
    {
        _input.MouseDeltaChanged += OnMouseDeltaChanged;
        _input.ScrollDeltaChanged += OnScrollDeltaChanged;
        _screens.Shop.ItemClicked += BuyItem;
        _plusSystem.ItemClicked += BuyItem;
        _screens.Popap.Applyed += OnApplyed;
    }

    private void BuyItem(int index)
    {
        var item = _enviroment.GetItem(index);
        if(item.Cost > _wallet.CoinCount)
            return;
        
        _audio.PlaySoundOneShot(TrackName.Click);
        
        _wallet.CoinCount -= item.Cost;
        _screens.CoinsUI.DrawCoins(_wallet.CoinCount);
        _coinsPerSecond += item.CoinsPerSecond;

        _screens.Shop.BuyItem(index);
        _plusSystem.Hide(index);
        
        var sequence = DOTween.Sequence();
        sequence.AppendCallback(() => _camera.MoveTo(item.transform.position));
        sequence.AppendInterval(0.5f);
        sequence.AppendCallback(() =>
        {
            item.Show();
            _audio.PlaySoundOneShot(TrackName.Pop);
        });
        sequence.AppendInterval(1f);
        sequence.AppendCallback(() => AddAvailableItemWithAnimation(_buyedIndex));
            
        _buyedIndex++;
    }

    private IEnumerator UpdateCoinsPerSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            AddCoins(_coinsPerSecond);
            _coinsPerSecondInfo.Registrate(_coinsPerSecond);
            _screens.CoinsPerSecondUI.DrawCoinsPerSecond(_coinsPerSecondInfo.Get());
        }
    }

    private void AddCoins(int coins)
    {
        _wallet.CoinCount += coins;
        _screens.CoinsUI.DrawCoins(_wallet.CoinCount);
    }

    private void OnMouseDeltaChanged(Vector2 direction)
    {
        _camera.Move(direction);
    }

    private void OnScrollDeltaChanged(float delta)
    {
        _camera.Zoom(delta);
    }

    private void OnApplyed()
    {
        AddCoins(200000);
        AddAvailableItemWithoutAnimation(0);
        _buyedIndex++;
        AddAvailableItemWithoutAnimation(1);
    }

    private void AddAvailableItemWithoutAnimation(int index)
    {
        var item = _enviroment.GetItem(index);
        _screens.Shop.AddItem(item);
        _plusSystem.AddItem(item);
    }

    private void AddAvailableItemWithAnimation(int index)
    {
        var item = _enviroment.GetItem(index);
        _screens.Shop.AddItemWithAnimation(item);
        _plusSystem.AddItemWithAnimation(item);
    }

    private void OnDisable()
    {
        _input.MouseDeltaChanged -= OnMouseDeltaChanged;
        _input.ScrollDeltaChanged -= OnScrollDeltaChanged;
        _screens.Shop.ItemClicked -= BuyItem;
        _plusSystem.ItemClicked -= BuyItem;
        _screens.Popap.Applyed -= OnApplyed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            AddCoins(50000);
        }
    }
}