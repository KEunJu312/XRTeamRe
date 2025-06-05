using UnityEngine;
using UnityEngine.Events;

public class InteractEventRelay : MonoBehaviour
{
    public UnityEvent onInteract;

    public void InvokeEvent()
    {
        onInteract?.Invoke();
    }
}
