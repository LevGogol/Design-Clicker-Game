using System;
using Azur.PlayableTemplate.Sound;
using DG.Tweening;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    [SerializeField] private Pluses pluses;
    [SerializeField] private Enviroment _enviroment;
    [SerializeField] private CameraFacade _camera;

    public event Action<Item> ItemBuyed = delegate(Item item) {  };
    public event Action LastBuyed = delegate() {  };
    
    private int currentGroup;

    private void OnEnable()
    {
        pluses.PlusClicked += BuyItem;
    }

    private void OnDisable()
    {
        pluses.PlusClicked -= BuyItem;
    }

    private void BuyItem(Item item)
    {
        pluses.Hide(item.Index);
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

    public void TryAddGroup()
    {
        var group = _enviroment.GetGroup(currentGroup);
        foreach (var item in group.Items)
        {
            if (item.Buyed == false)
                return;
        }
        
        currentGroup++;
        
        if (currentGroup >= _enviroment.GroupCount)
        {
            LastBuyed.Invoke();
            return;
        }
        
        DOVirtual.DelayedCall(1.5f, () => AddGroup(currentGroup));
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
        pluses.AddPlusWithAnimation(item);
    }
}