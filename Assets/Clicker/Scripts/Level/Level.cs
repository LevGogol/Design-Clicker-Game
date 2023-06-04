using System;
using DG.Tweening;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private string _title;
    [SerializeField] private Sprite _openSprite;
    [SerializeField] private Sprite _closeSprite;
    [SerializeField] private Color _backgroundColor;
    [SerializeField] private Vector3 _boundsSize;
    [SerializeField] private ItemSystem _itemSystem;
    [SerializeField] private MeshRenderer _background;

    private InputFacade _input;
    private Screens _screens;
    private CameraFacade _cameraFacade;

    public event Action Ended;

    public string Title => _title;
    public Sprite OpenSprite => _openSprite;
    public Sprite CloseSprite => _closeSprite;
    public bool IsComplete;
    public int Index;

    private void Start()
    {
        _itemSystem.AddGroup(0);
        _cameraFacade.Camera.backgroundColor = _backgroundColor;
        _background.material.color = _backgroundColor;
        _cameraFacade.SetBoundsSize(_boundsSize);
    }

    public void Initialize(InputFacade input, Screens screens, CameraFacade cameraFacade)
    {
        _input = input;
        _screens = screens;
        _cameraFacade = cameraFacade;
    }

    private void End()
    {
        Ended?.Invoke();
    }

    private void OnEnable()
    {
        _itemSystem.ItemBuyed += OnItemBuyed;
        _itemSystem.LastBuyed += ShowNextButton;
        _input.MouseDeltaChanged += OnMouseDeltaChanged;
    }

    private void OnDisable()
    {
        _itemSystem.ItemBuyed -= OnItemBuyed;
        _itemSystem.LastBuyed -= ShowNextButton;
        _input.MouseDeltaChanged -= OnMouseDeltaChanged;
    }

    private void ShowNextButton()
    {
        DOVirtual.DelayedCall(1.5f, () =>
        {
            _screens.ShowNextButton(End);
        });
    }

    private void OnMouseDeltaChanged(Vector2 direction)
    {
        _cameraFacade.Move(direction);
    }

    private void OnItemBuyed(Item item)
    {
        _cameraFacade.MoveTo(item.transform.position);
        _itemSystem.TryAddGroup();
    }
    
    #if UNITY_EDITOR
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            End();
        }
    }
#endif
}