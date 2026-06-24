using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColliderMonitor : MonoBehaviour
{
    public Action<Collider2D> onTriggerEnter;
    public Action<Collider2D> onTriggerExit;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        onTriggerEnter?.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        onTriggerExit?.Invoke(other);
    }
}
