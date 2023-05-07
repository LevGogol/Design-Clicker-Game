using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private int _coinsPerSecond;
    [SerializeField] private InputFacade _input;
    [SerializeField] private Screens _screens;
    [SerializeField] private CameraFacade _camera;
    [SerializeField] private ItemSystem _itemSystem;

    private Wallet _wallet;
    private CoinsPerSecondInfo _coinsPerSecondInfo;

    private void Awake()
    {
        _wallet = new Wallet();
        _itemSystem.Initialize(_wallet);
        // _coinsPerSecondInfo = new CoinsPerSecondInfo();
        // _screens.CoinsUI.ForceDrawCoins(_wallet.CoinCount);
        // _screens.CoinsPerSecondUI.DrawCoinsPerSecond(_coinsPerSecondInfo.Get());
        // _screens.Popap.Show();

        // StartCoroutine(UpdateCoinsPerSecond());
    }

    private void OnEnable()
    {
        _input.MouseDeltaChanged += OnMouseDeltaChanged;
        _screens.Popap.Applyed += OnApplyed;
        _itemSystem.ItemBuyed += ItemSystemOnItemBuyed;
    }

    private void ItemSystemOnItemBuyed(Item item)
    {
        _coinsPerSecond += item.CoinsPerSecond;
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

    private void OnApplyed()
    {
        AddCoins(200000);
        _itemSystem.AddGroup(0);
    }

    private void OnDisable()
    {
        _input.MouseDeltaChanged -= OnMouseDeltaChanged;
        _screens.Popap.Applyed -= OnApplyed;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            AddCoins(50000);
        }
    }
#endif
}