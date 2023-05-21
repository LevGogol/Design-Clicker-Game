using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private ItemSystem _itemSystem;
    [SerializeField] private Color _backgroundColor;
    
    private InputFacade _input;
    private Screens _screens;
    private CameraFacade _cameraFacade;

    public event Action Ended = delegate {  };

    private void Start()
    {
        _itemSystem.AddGroup(0);
        _cameraFacade.Camera.backgroundColor = _backgroundColor;
    }

    public void Initialize(InputFacade input, Screens screens, CameraFacade cameraFacade)
    {
        _input = input;
        _screens = screens;
        _cameraFacade = cameraFacade;
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
        _screens.ShowNextButton(() => Ended.Invoke());
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
            Ended.Invoke();
        }
    }
#endif
}