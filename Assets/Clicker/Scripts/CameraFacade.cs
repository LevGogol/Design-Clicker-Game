using System;
using DG.Tweening;
using UnityEngine;

public class CameraFacade : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Transform _objectAtCenter;

    private Vector3 _offsetFromCenterObject;

    private void Awake()
    {
        _offsetFromCenterObject = transform.position - _objectAtCenter.position;
    }

    public bool TryMove(Vector3 direction)
    {
        var oldPosition = transform.position;
        
        var nextPositionX = transform.position + Vector3.right * direction.x;
        if(BoxContainsPoint(_boxCollider.transform, _boxCollider.center, _boxCollider.size, nextPositionX))
            transform.position = nextPositionX;
        
        var nextPositionY = transform.position + Vector3.forward * direction.z;
        if(BoxContainsPoint(_boxCollider.transform, _boxCollider.center, _boxCollider.size, nextPositionY))
            transform.position = nextPositionY;

        return oldPosition == transform.position;
    }

    public void MoveTo(Vector3 target)
    {
        transform.DOMove(target + _offsetFromCenterObject, 0.5f).SetEase(Ease.OutSine);
    }

    private bool BoxContainsPoint(Transform colliderTransform, Vector3 offset, Vector3 colliderSize, Vector3 point)
    {
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