using System;
using UnityEngine;


public class InputFacade : MonoBehaviour
{
    [SerializeField] private Vector2 Sensitive;

    private Vector3 _startPoint;
    private Vector3 _lastPosition;
    private Vector2 _delta;
    private Vector2 _offset;

    public event Action DownTouched;
    public event Action UpTouched;
    public event Action<Vector2> MouseDeltaChanged;
    public event Action<Vector2> MouseOffsetChanged;
    public event Action<float> ScrollDeltaChanged;

    public bool IsTouch => UnityEngine.Input.GetMouseButton(0);
    public Vector2 MouseOffset() => _offset * Sensitive;
    public Vector2 MouseDelta() => _delta * Sensitive;

    private void Update()
    {
        var mousePositionScreen = UnityEngine.Input.mousePosition;

        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            _startPoint = mousePositionScreen;

            _lastPosition = mousePositionScreen;
            DownTouched?.Invoke();
        }

        if (UnityEngine.Input.GetMouseButton(0))
        {
            _offset = mousePositionScreen - _startPoint;
            _offset.x /= Screen.width;
            _offset.y /= Screen.height;

            _delta = (mousePositionScreen - _lastPosition);
            _delta.x /= Screen.width;
            _delta.y /= Screen.height;

            _lastPosition = mousePositionScreen;
        }

        if (UnityEngine.Input.GetMouseButtonUp(0))
        {
            _delta = Vector2.zero;
            _offset = Vector2.zero;

            UpTouched?.Invoke();
        }

        MouseDeltaChanged?.Invoke(MouseDelta());
        MouseOffsetChanged?.Invoke(MouseOffset());
        
        ScrollDeltaChanged?.Invoke(Mathf.Clamp(Input.mouseScrollDelta.y, -0.5f, 0.5f));
        
    }
}