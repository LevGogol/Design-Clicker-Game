using System;
using DG.Tweening;
using UnityEngine;

public class CameraFacade : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Transform _objectAtCenter;
    [SerializeField] private float _power;
    
    private Vector3 _offsetFromCenterObject;
    private Vector3 _startPosition;

    private void Awake()
    {
        _offsetFromCenterObject = transform.position - _objectAtCenter.position;
        _startPosition = transform.localPosition;
    }

    private void LateUpdate()
    {
        if (!BoxContainsPoint(_boxCollider, transform.localPosition))
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _startPosition, _power);
        }
    }

    public void Move(Vector3 direction)
    {
        if (direction == Vector3.zero) 
            return;

        direction = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, 0) * direction;
        transform.localPosition += direction;
    }

    public void MoveTo(Vector3 target)
    {
        target.y = _objectAtCenter.position.y;
        transform.DOMove(target + _offsetFromCenterObject, 0.5f).SetEase(Ease.OutSine);
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
        else 
            return false;
    }
}