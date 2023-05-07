using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private InputFacade _input;
    [SerializeField] private Screens _screens;
    [SerializeField] private CameraFacade _camera;
    [SerializeField] private ItemSystem _itemSystem;
    
    private void Awake()
    {
        _itemSystem.AddGroup(0);
    }

    private void OnEnable()
    {
        _input.MouseDeltaChanged += OnMouseDeltaChanged;
        _itemSystem.ItemBuyed += TryAddSecondGroup;
        _itemSystem.LastBuyed += ShowNextButton;
    }

    private void ShowNextButton()
    {
        _screens.ShowNextButton();
    }

    private void TryAddSecondGroup(Item obj)
    {
        _itemSystem.TryAddGroup();
    }

    private void OnMouseDeltaChanged(Vector2 direction)
    {
        _camera.Move(direction);
    }

    private void OnDisable()
    {
        _input.MouseDeltaChanged -= OnMouseDeltaChanged;
    }
}