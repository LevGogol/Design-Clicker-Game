using System;
using UnityEngine;

public class InputFacade : MonoBehaviour
{
    public event Action Downed;
        
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Downed?.Invoke();
        }
    }
}