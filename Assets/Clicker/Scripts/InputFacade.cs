using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFacade : MonoBehaviour
{
    [SerializeField] private Vector2 _sensitive;
    [SerializeField] private float _scrollSensiteve;
    // [SerializeField] private TextMeshProUGUI _textMesh;
    // [SerializeField] private Slider _slider;

    private Vector3 _startPoint;
    private Vector3 _lastPosition;
    private Vector2 _delta;
    private Vector2 _offset;
    private float _scrollPreviousOffset;
    private int _fingerID = -1;
    private Vector3 _mousePositionScreen;

    private const float ScrollSensetiveForSmartphone = 0.07f;

    public event Action DownTouched;
    public event Action UpTouched;
    public event Action<Vector2> MouseDeltaChanged;
    public event Action<Vector2> MouseOffsetChanged;
    public event Action<float> ScrollDeltaChanged;

    public bool IsTouch => UnityEngine.Input.GetMouseButton(0);
    public Vector2 MouseOffset() => _offset * _sensitive;
    public Vector2 MouseDelta() => _delta * _sensitive;

    private void Update()
    {
        if (Input.touches.Length != 0)
        {
            if (_fingerID == -1)
                _fingerID = Input.touches[0].fingerId;

            if(_fingerID == Input.touches[0].fingerId)
                _mousePositionScreen = Input.touches[0].position;
        }
        else
        {
            _mousePositionScreen = Input.mousePosition;
            _fingerID = -1;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            _startPoint = _mousePositionScreen;

            _lastPosition = _mousePositionScreen;
            DownTouched?.Invoke();
        }

        if (UnityEngine.Input.GetMouseButton(0))
        {
            _offset = _mousePositionScreen - _startPoint;
            _offset.x /= Screen.width;
            _offset.y /= Screen.height;

            _delta = (_mousePositionScreen - _lastPosition);
            _delta.x /= Screen.width;
            _delta.y /= Screen.height;
            
            _lastPosition = _mousePositionScreen;
        }

        if (UnityEngine.Input.GetMouseButtonUp(0))
        {
            _delta = Vector2.zero;
            _offset = Vector2.zero;
            _scrollPreviousOffset = 0f;

            UpTouched?.Invoke();
        }

        if (Input.touchCount == 2)
        {
            _scrollSensiteve = ScrollSensetiveForSmartphone;
            
            var touch1 = Input.touches[0];
            var touch2 = Input.touches[1];

            var offset = Vector2.Distance(touch2.position, touch1.position);
            var delta = _scrollPreviousOffset - offset;
            if(_scrollPreviousOffset != 0f)
                ScrollDeltaChanged?.Invoke(-delta * _scrollSensiteve);
            _scrollPreviousOffset = offset;
        }
        else
        {
            ScrollDeltaChanged?.Invoke(Input.mouseScrollDelta.y * _scrollSensiteve);
        }

        MouseDeltaChanged?.Invoke(MouseDelta());
        MouseOffsetChanged?.Invoke(MouseOffset());
    }
}