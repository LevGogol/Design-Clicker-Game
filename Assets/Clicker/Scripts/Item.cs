using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _coinsPerSecond;
    [SerializeField] private int _cost;
}