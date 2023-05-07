using System;
using Azur.PlayableTemplate.Sound;
using DG.Tweening;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    [SerializeField] private PlusSystem _plusSystem;
    [SerializeField] private Screens _screens;
    [SerializeField] private Enviroment _enviroment;
    [SerializeField] private CameraFacade _camera;

    public event Action<Item> ItemBuyed = delegate(Item item) {  };
    
    private Wallet _wallet;
    private int currentGroup;

    private Shop _shop => _screens.Shop;

    private void OnEnable()
    {
        _plusSystem.ItemClicked += BuyItem;
        _shop.ItemClicked += BuyItem;
    }

    private void OnDisable()
    {
        _plusSystem.ItemClicked -= BuyItem;
        _shop.ItemClicked -= BuyItem;
    }

    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;
    }
    
    private void BuyItem(int index)
    {
        var item = _enviroment.GetItem(index);
        if(item.Cost > _wallet.CoinCount)
            return;

        _wallet.CoinCount -= item.Cost;
        _screens.CoinsUI.DrawCoins(_wallet.CoinCount);

        _screens.Shop.BuyItem(index);
        _plusSystem.Hide(index);
        item.Buyed = true;

        Audio.Instance.PlaySoundOneShot(TrackName.Click);
        PlayOpenAnimation(item);

        ItemBuyed.Invoke(item);
    }

    private void PlayOpenAnimation(Item item)
    {
        var sequence = DOTween.Sequence();
        sequence.AppendCallback(() => _camera.MoveTo(item.transform.position));
        sequence.AppendInterval(0.5f);
        sequence.AppendCallback(() =>
        {
            item.Show();
            Audio.Instance.PlaySoundOneShot(TrackName.Pop);
        });
        sequence.AppendInterval(1f);
        sequence.AppendCallback(TryAddGroup);
    }

    private void TryAddGroup()
    {
        var group = _enviroment.GetGroup(currentGroup);
        foreach (var item in group.Items)
        {
            if (item.Buyed == false)
                return;
        }
        
        currentGroup++;
        AddGroup(currentGroup);
    }

    public void AddGroup(int index)
    {
        var group = _enviroment.GetGroup(index);
        foreach (var item in group.Items)
        {
            AddAvailableItem(item.Index);
        }
    }

    private void AddAvailableItem(int index)
    {
        var item = _enviroment.GetItem(index);
        _screens.Shop.AddItemWithAnimation(item);
        _plusSystem.AddItemWithAnimation(item);
    }
}