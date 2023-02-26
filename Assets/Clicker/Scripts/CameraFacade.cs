using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFacade : MonoBehaviour
{
    [SerializeField] private Bounds _bounds;
    [SerializeField] private BoxCollider _boxCollider;
    
    public void TryMove(Vector3 direction)
    {
        var nextPositionX = transform.position + Vector3.right * direction.x;
        if(BoxContainsPoint(_boxCollider.transform, _boxCollider.center, _boxCollider.size, nextPositionX))
            transform.position = nextPositionX;
        
        var nextPositionY = transform.position + Vector3.forward * direction.z;
        if(BoxContainsPoint(_boxCollider.transform, _boxCollider.center, _boxCollider.size, nextPositionY))
            transform.position = nextPositionY;
    }

    public bool BoxContainsPoint(Transform colliderTransform, Vector3 offset, Vector3 colliderSize, Vector3 point)
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