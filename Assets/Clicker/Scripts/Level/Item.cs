using DG.Tweening;
using UnityEngine;

public class Item : MonoBehaviour
{
    [HideInInspector] public int Index;
    
    public bool Buyed { get; set; }

    public void Show()
    {
        gameObject.SetActive(true);

        transform.DOScale(transform.localScale, 1f).SetEase(Ease.OutBounce).From(0f);
    }
}