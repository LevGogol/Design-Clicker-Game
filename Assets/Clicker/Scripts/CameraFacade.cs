using DG.Tweening;
using UnityEngine;

public class CameraFacade : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Transform _objectAtCenter;
    [SerializeField] private float _returnPower;
    [SerializeField] private float _movePower;
    [SerializeField] private float _zoomPower;
    [SerializeField] private float _minZoom;
    [SerializeField] private float _maxZoom;
    [SerializeField] private Camera _camera;
    
    private Vector3 _offsetFromCenterObject;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _targetZoom;

    private void Awake()
    {
        _offsetFromCenterObject = transform.position - _objectAtCenter.position;
        _startPosition = transform.localPosition;
        _targetPosition = transform.localPosition;
    }

    private void LateUpdate()
    {
        if (!BoxContainsPoint(_boxCollider, _targetPosition))
        {
            _targetPosition = Vector3.Lerp(_targetPosition, _startPosition, _returnPower);
        }
        
        transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPosition, _movePower);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _targetZoom, _zoomPower);
    }

    public void Move(Vector3 direction)
    {
        if (direction == Vector3.zero) 
            return;

        var invertedDirection = new Vector3(-direction.x, 0, -direction.y);
        var invertedRotatedDirection = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, 0) * invertedDirection;
        _targetPosition += invertedRotatedDirection;
    }

    public void MoveTo(Vector3 target)
    {
        target.y = _objectAtCenter.position.y;
        DOVirtual.Vector3(_targetPosition, target + _offsetFromCenterObject, 0.5f, value => _targetPosition = value).SetEase(Ease.OutSine);
    }

    public void Zoom(float delta)
    {
        var result = _camera.orthographicSize + -delta;

        if (result < _minZoom)
        {
            _targetZoom = _minZoom;
            return;
        }

        if (result > _maxZoom)
        {
            _targetZoom = _maxZoom;
            return;
        }
        
        _targetZoom = result;
    }

    private bool BoxContainsPoint(BoxCollider boxCollider, Vector3 point)
    {
        var colliderTransform = boxCollider.transform;
        var offset = boxCollider.center;
        var colliderSize = boxCollider.size;
        
        Vector3 localPos = colliderTransform.InverseTransformPoint(point);
 
        localPos -= offset;
 
        if (Mathf.Abs(localPos.x) < (colliderSize.x/2) 
            && Mathf.Abs(localPos.y) < (colliderSize.y/2) 
            && Mathf.Abs(localPos.z) < (colliderSize.z/2))  
            return true;
        
        return false;
    }
}
